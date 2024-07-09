namespace Bank_System;

public class Bank 
{
    public readonly string Name;
    public readonly CurrencyType Currency;
    public Dictionary<CurrencyType, double> Currencies = new Dictionary<CurrencyType, double>();
    public readonly double FeeSending; //коммка на отправку
    public readonly double FeeReceipt; //коммка на прием
    public List<MainUser> Users = new List<MainUser>();
    
    
    public Bank(string name, string currency, double feeSending, double feeReceipt,List<MainUser> list = null)
    {
        Users = list;
        Name = name;
        Currency = Common.ParseStringToCurrencyType(currency);
        if (feeSending < 0.0 || feeSending > 100.0)
            throw new ArgumentException("Fee can't be less than 0 and greater than 100");
        FeeSending = feeSending;
      
        if (feeReceipt < 0.0 || feeReceipt > 100.0)
            throw new ArgumentException("Fee can't be less than 0 and greater than 100");
        FeeReceipt = feeReceipt;
    }
}