

namespace Bank_System;

class Program
{
    static void Main(string[] args)
    {
        



        
        BankUser user = new BankUser("123", "123", "123", "+380505005050", "123", new DateTime(2003,4,8));

        //Console.WriteLine("Start: ");
        //Console.WriteLine(user);
        //Console.WriteLine("=============");

        //user.ChangeBankUser();

        //DateTime date = new DateTime(2000,12,20);

        //Console.WriteLine($"{date.Year},{date.Month}, {date.Day}");




        //Console.WriteLine("Result: ");
        //Console.WriteLine(user);

        Admin adm = new Admin("Alex","admin","0000");

        Console.WriteLine(adm);


        adm.ChangeAdmin();

        Console.WriteLine(adm);

        

        Console.ReadLine();
    }
}