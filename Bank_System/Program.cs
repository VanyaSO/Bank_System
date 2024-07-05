namespace Bank_System;

class Program
{
    static void Main()
    {
        Common.Bank = new Bank("MonoBank", 2.0, 2.0);
        Console.ReadKey();
        // MainMenu.Menu();
        // Card card = new Card("1234", CurrencyType.UAH);
        // Card card1 = new Card("1234", CurrencyType.UAH);
        // card.Deposit(1500);
        // card.Withdraw(500);
        // card.BlockCard();
        // card.UnblockCard();
        // card.Transfer(card1, 500, CurrencyType.UAH, "Alex");
        // Console.WriteLine(card.ToString());
        // card.SetPinCode("4322");
        // card.VerifyPinCode("3132");
        // card.VerifyPinCode("1132");
        // card.VerifyPinCode("3432");
        // Console.WriteLine(card.ToString());
        // ivan text commit
    }
}