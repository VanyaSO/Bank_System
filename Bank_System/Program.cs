﻿

namespace Bank_System;

class Program
{
    static void Main(string[] args)
    {
        ////Date date = new Date(1,1,2022);
        //date.MakeDate();



        //BankUser user = new BankUser();

        //Date date = new Date(); 
        //Console.WriteLine(date.ToString());

        //date.CreateDate();

        //Console.WriteLine(date);

        //BankUser user = new BankUser();
        //Console.WriteLine(user);

        //user.Registration();

        //Console.WriteLine(user);

        //string phoneNumb = "+380635955895";
        //phoneNumb.ShowHiddenNumber();
        BankUser user = new BankUser("123", "123", "123", "+380505005050", "123", new DateTime(2003,4,8));

        Console.WriteLine("Start: ");
        Console.WriteLine(user);
        Console.WriteLine("=============");

        user.ChangeBankUser();

        //DateTime date = new DateTime(2000,12,20);

        //Console.WriteLine($"{date.Year},{date.Month}, {date.Day}");




        Console.WriteLine("Result: ");
        Console.WriteLine(user);



        Console.ReadLine();
    }
}