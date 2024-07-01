namespace Bank_System;

class Program
{
    static void Main()
    {
        MainMenu.Menu();
        Card card = new Card("1234", CurrencyType.UAH);
    }
}