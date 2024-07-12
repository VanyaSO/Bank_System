namespace Bank_System;

public class Bank 
{
    public readonly string Name;
    public readonly CurrencyType Currency;
    public Dictionary<CurrencyType, decimal> Currencies;
    public readonly double FeeSending; //коммка на отправку
    public readonly double FeeReceipt; //коммка на прием
    public List<MainUser> Users;
    
    
    public Bank(string name, CurrencyType currency, double feeSending, double feeReceipt, List<MainUser>? list = null)
    {
        Users = list ?? new List<MainUser>();
        Name = name;
        Currency = currency;
        Currencies = new Dictionary<CurrencyType, decimal>();
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
                Console.WriteLine();
            }
        }
    }

 

    public static BankUser GetUserByData(string findData)
    {
        foreach (var user in Common.Bank.Users)
        {
            if (user is BankUser bankUser && bankUser.UserRole == Role.BankUser)
            {
                if (findData == bankUser.Name || findData == bankUser.ID)
                {
                    return bankUser;
                }
            }
        }

        return null;
    }


    public static void ShowAllCommision()
    {
        double fullSum = 0;
        foreach (BankUser user in Common.Bank.Users)
        {
            if(user is BankUser bankUser)
            {
                double sum = user.GetSumOfComisionByUser();
                fullSum += sum;
                Console.WriteLine($"{user.Name}: {sum}");

            }
        }

        Console.WriteLine($"Общая сумма заработка на комиссии: {fullSum}");

    }

}