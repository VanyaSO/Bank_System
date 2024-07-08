using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bank_System;

public static class Query
{
    static void Deserialize(string path, ref List<Currency> list)
    {
        var deserializedObj = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(path));

        string dateLastUpdate = deserializedObj.dateLastUpdate.ToString();
        string dateToday = DateOnly.FromDateTime(DateTime.Today).ToString("dd.MM.yyyy");
        if (dateLastUpdate.CompareTo(dateToday) != 0)
        {
            throw new Exception("Курсы валют не актальны");
        }

        list = JsonConvert.DeserializeObject<List<Currency>>(deserializedObj.listCurrency.ToString());
    }
    public static async void GetСurrencyRates()
    {
        string path = Config.CurrencyRatesPath;
        // список валют банка 
        List<Currency> listCurrency = new List<Currency>();

        try
        {
            if (!File.Exists(path)) throw new Exception();
            Deserialize(path, ref listCurrency);
        }
        catch (Exception e)
        {
            // делаем запрос и получаем новые данные
            string url = Config.ApiCurrencyRates;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content.Length > 0)
                {
                    // список все полученых курсов
                    List<Currency> listGetCurrency = JsonConvert.DeserializeObject<List<Currency>>(content);
                    // новая дата носледнего обновленич
                    string dateLastUpdate = DateOnly.FromDateTime(DateTime.Today).ToString("dd.MM.yyyy");

                    foreach (var currency in listGetCurrency)
                    {
                        if (Enum.IsDefined(typeof(CurrencyType), currency.Cc))
                            listCurrency.Add(currency);
                    }

                    // курс валюты по умолчанию
                    listCurrency.Add(new Currency(Common.Bank.Currency, 1));

                    string json =
                        JsonConvert.SerializeObject(new { dateLastUpdate, listCurrency }, Formatting.Indented);
                    File.WriteAllText(path, json);
                }
            }
        }
        finally
        {
            // сохраняем все курсы валют
            foreach (var currency in listCurrency)
            {
                Common.Bank.Currencies.Add(currency.Cc, currency.Rate);
            }
        }
    }
}