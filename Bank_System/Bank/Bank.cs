

namespace Bank_System;

public class Bank 
{
    public string BankName { get; private set; }
    public CurrencyType DefaultBankCurrency { get; private set; }
    public List<CurrencyType> CurrencyList;
    public float FeeSending { get; private set; } //коммка на отправку
    public float FeeReceipt{ get; private set; } //коммка на прием
    // public List<User> Users = new List<User>(); TODO: Стянуть не забудь класс юзеров.
    public List<MainUser> Users = new List<MainUser>();
    
    

    public Bank(string name, CurrencyType currency, List<CurrencyType> list, float feeSending, float feeReceipt,List<MainUser> users)
    {
        BankName = name;
        DefaultBankCurrency = currency;
        CurrencyList = list;

        Users = users;

        if (feeSending > 100 || feeSending < 0)
            FeeReceipt = feeReceipt;
        else throw new ArgumentException("Fee can't be less than 0 and greater than 100");
        
        if (feeReceipt > 100 || feeReceipt < 0)
            FeeSending = feeSending;
        else throw new ArgumentException("Fee can't be less than 0 and greater than 100");
    }

    public Bank(string name, CurrencyType currency, float feeSending, float feeReceipt, List<MainUser> users)///жобавил я
    {
        BankName = name;
        DefaultBankCurrency = currency;
        

        Users = users;

            FeeReceipt = feeReceipt;

        
            FeeSending = feeSending;
       
    }


}