namespace Bank_System;

public struct TransactionCardInfo
{
    public string CardNumber { get; }
    public CurrencyType Currency { get; }

    public TransactionCardInfo(string cardNumber, CurrencyType currency)
    {
        CardNumber = cardNumber;
        Currency = currency;
    }
}