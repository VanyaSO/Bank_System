namespace Bank_System;

public struct Currency
{
    public CurrencyType Cc { get; init; }
    public decimal Rate { get; init; }

    public Currency(CurrencyType cc, decimal rate)
    {
        Cc = cc;
        Rate = rate;
    }

    public override string ToString() => $"{Cc} : {Rate}";
}