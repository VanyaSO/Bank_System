using System.Diagnostics;

namespace Bank_System;

public static class Common
{
    public static Bank Bank;
    public static MainUser User;
    public static Random Random = new Random();
    
    public const string ApiCurrencyRatesJson = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
    public const string PathCurrencyRatesJson = "rates.json";
    public const string PathBankFileBin = "bank.dat";
    public const string PathUsersFileBin = "users.dat";
    public const string PathStatisticsDir = "../../../Статистика";
    
    public static void StartProgram()
    {
        if (File.Exists(PathBankFileBin))
        {
            FileSystem.LoadBank();
            FileSystem.LoadUsers();
        }
        else 
            DefaultData();
        
        Query.GetСurrencyRates();
    }
    
    public static void FinishProgram()
    {
        FileSystem.SaveBank();
        FileSystem.SaveUsers();
    }
    
    public static int GetAction(int maxVarAction, int minVarAction)
    {
        int action;

        if (!Int32.TryParse(Console.ReadLine(), out action))
            throw new Exception("Введённое значение не является числом");

        if (action < minVarAction || action > maxVarAction)
            throw new Exception("Введённое число выходит за пределы допустимого диапазона");

        return action;
    }

    // чтобы были дефолтные данные если нету файлов
    public static void DefaultData()
    {
        Bank = new Bank("MonoBank", CurrencyType.UAH,2.0, 2.0,
            new List<MainUser>()
            {
                new Admin("Иванов Иван", "admin", "admin"),
                new BankUser("Анна Петрова", "anna", "пароль123", "380952144378", "1", new DateOnly(1995, 8, 21), 
                    new List<Card>()
                    {
                        new Card("1111", CurrencyType.EUR, 500.00m)
                    }),
                new BankUser("Павел Сидоров", "pavel", "пароль123", "380982367350", "2", new DateOnly(1980, 3, 10),
                    new List<Card>()
                    {
                        new Card("1111", CurrencyType.UAH, 12000.00m),
                        new Card("1111", CurrencyType.USD, 20.00m)
                    })
            });
    }
}
