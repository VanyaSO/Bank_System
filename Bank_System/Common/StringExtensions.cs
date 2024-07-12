namespace Bank_System;

public static class StringExtensions
{
    public static CurrencyType ParseToCurrencyType(this string str) => (CurrencyType)Enum.Parse(typeof(CurrencyType), str);
    public static Role ParseToRoleType(this string str) => (Role)Enum.Parse(typeof(Role), str);
    public static CardStatus ParseToCardStatus(this string str) => (CardStatus)Enum.Parse(typeof(CardStatus), str);
}