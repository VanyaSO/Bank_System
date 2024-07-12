namespace Bank_System;

public class Bank 
{
    public readonly string Name;
    public readonly CurrencyType Currency;
    public Dictionary<CurrencyType, double> Currencies = new Dictionary<CurrencyType, double>();
    public readonly double FeeSending; //коммка на отправку
    public readonly double FeeReceipt; //коммка на прием
    public List<MainUser> Users = new List<MainUser>();
    
    
    public Bank(string name, string currency, double feeSending, double feeReceipt,List<MainUser> list)
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

    public static void ShowAllUsers()
    {
        foreach (MainUser user in Common.Bank.Users)
        {
            if (user.UserRole == Role.BankUser)
            {
                (user as BankUser).ShowUserInfo();
            }
        }
    }

    public static BankUser GetUserByData(string findData)
    {
        foreach (BankUser user in Common.Bank.Users)
        {
            if (findData == user.Name || findData == user.ID)
            {
                return user;
            }
        }

        return null;
    }


    public static void ShowAllCommision()
    {
        double fullSum = 0;
        foreach (BankUser user in Common.Bank.Users)
        {

            double sum = user.GetSumOfComisionByUser();
            fullSum += sum;
            Console.WriteLine($"{user.Name}: {sum}");
        }

        Console.WriteLine($"Общая сумма заработка на комиссии: {fullSum}");

    }

}