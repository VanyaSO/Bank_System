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

        //DateTime date = new DateTime(2000, 12, 12);

        //List<Card> cards = new List<Card>();

        ////BankUser user = new BankUser("1","1","1","1","1",date,cards);
        //try
        //{

        //    user.OpenNewCard();

        //    Console.WriteLine("Result: ");

        //    user.ShowAllCards();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}
        

        Admin adm1 = new Admin("Admin1","adm1","0000");
        
        BankUser user1 = new BankUser("User1", "user1", "1111", "1", "0101", new DateTime(2000, 01, 01));
        BankUser user2 = new BankUser("User2", "user2", "2222", "2", "0202", new DateTime(2002, 02, 02));
        BankUser user3 = new BankUser("User3", "user3", "3333", "3", "0303", new DateTime(2003, 03, 03));


        List<MainUser> list = new List<MainUser?>() { adm1, user1, user2, user3 };

        Bank bank1 = new Bank("bank1", CurrencyType.UAH,10.0f,5.6f,list);

        Common.CurrentBank = bank1;

        MainMenu.Menu();


        //try
        //{
        //    MainUser? user = MainUser.LogIn();

        //    Console.WriteLine(user);

        //}
        //catch (Exception ex)
        //{
        //    Message.ErrorMessage(ex.Message);
        //}


        //Console.WriteLine(adm1);

        //Console.WriteLine("Result: ");
        //adm1.ChangeAdminPass();


        //Console.WriteLine(adm1);


        //MainMenu.Menu();


        Console.ReadLine();
    }
}