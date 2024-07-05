namespace Bank_System;

public class Bank 
{
    public readonly string BankName;
    public readonly CurrencyType Currency = CurrencyType.UAH;
    public Dictionary<CurrencyType, decimal> Currencies = new Dictionary<CurrencyType, decimal>();
    public readonly double FeeSending; //коммка на отправку
    public readonly double FeeReceipt; //коммка на прием
    // public List<User> Users = new List<User>(); TODO: Стянуть не забудь класс юзеров.
    public List<MainUser> Users = new List<MainUser>();
    
    
    public Bank(string name, double feeSending, double feeReceipt)
    {
        BankName = name;
        if (feeSending < 0.0 || feeSending > 100.0)
            throw new ArgumentException("Fee can't be less than 0 and greater than 100");
        FeeSending = feeSending;
      
        if (feeReceipt < 0.0 || feeReceipt > 100.0)
            throw new ArgumentException("Fee can't be less than 0 and greater than 100");
        FeeReceipt = feeReceipt;
    }
}