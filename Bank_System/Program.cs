namespace Bank_System;

class Program
{
    static void Main()
    {
        //MainMenu.Menu();
        //Card card = new Card("1234", CurrencyType.UAH);
        //Card card1 = new Card("1234", CurrencyType.UAH);
        //card.Deposit(1500);
        //card.Withdraw(500);
        //card.BlockCard();
        //card.UnblockCard();
        //card.Transfer(card1, 500, CurrencyType.UAH, "Alex");
        //Console.WriteLine(card.ToString());
        //card.SetPinCode("4321");
        //card.VerifyPinCode("3432");
        //card.VerifyPinCode("4321");
        // ivan text commit

        DateTime date = new DateTime(2000, 12, 12);

        List<Card> cards = new List<Card>();

        BankUser user = new BankUser("1","1","1","1","1",date,cards);
        try
        {

            user.OpenNewCard();

            Console.WriteLine("Result: ");

            user.ShowAllCards();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }
}