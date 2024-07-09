namespace Bank_System;

public static class Common
{
    public static Bank Bank;
    public static MainUser User;
    public static Random Random = new Random();
    
    public const string ApiCurrencyRates = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
    public const string PathBankDataDir = "./BankData";
    public const string PathCurrencyRates = "rates.json";
    public const string PathBankData = "bank.dat";
    
    public static void StartProgram()
    {
        Query.GetСurrencyRates();
    }
    
    public static void FinishProgram()
    {
        FileSystem.SaveBankData();
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

    public static CurrencyType ParseStringToCurrencyType(string str) => (CurrencyType)Enum.Parse(typeof(CurrencyType), str);
    
    public static string CreatePath(string path, string dir = PathBankDataDir)
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        return Path.Combine(dir, path);
    }

}