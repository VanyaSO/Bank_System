using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal static class BankUserInterface
    {

        public static void Registration(this BankUser user)
        {
            try
            {

                Console.Write("Enter your name: ");
                if (string.IsNullOrEmpty(user.FullName = Console.ReadLine())) //проверка на пустую строку
                {
                    throw new Exception("Name cant be empty");
                }

                Console.Write("Enter new login: ");
                if (string.IsNullOrEmpty(user.Login = Console.ReadLine()))
                {
                    throw new Exception("Login cant be empty");
                }

                Console.Write("Enter new password: ");

                if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
                {
                    throw new Exception("Password cant be empty");
                }

                Console.Write("Enter you bd date: ");
                string date;
                if(string.IsNullOrEmpty(date = Console.ReadLine()))
                {
                    throw new Exception("Date cant be empty");
                }
                else
                {

                    string[] parts = date.Split(' ');
                    int day = Convert.ToInt16(parts[0]);
                    int month = Convert.ToInt16(parts[1]);
                    int year = Convert.ToInt16(parts[2]);


                    user.BDate = new DateTime(year, month, day);
                }



                //Date newDate = new Date();
                //newDate.CreateDate();
                //user.UserBdayDate = newDate;

                Console.Write("Enter your personal ID [or press [Enter] for create it automatically]: ");
                if (string.IsNullOrEmpty(user.ID = Console.ReadLine()))
                {
                    user.ID = createNewId();
                }


                Console.Write("Enter your phone: ");
                if (string.IsNullOrEmpty(user.PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Phone cant be empty");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }




        }

        private static string createNewId()
        {
            Random rand = new Random();
            int res = rand.Next(1000, 9999);
            return Convert.ToString(res);
        }

        public static void ChangeBankUser(this BankUser user)//общая для изменения доступных полей
        {

            Console.WriteLine("1 - Change phone number\t2 - Change password");
            string choice;
            try
            {

                if (string.IsNullOrEmpty(choice = Console.ReadLine()))
                {
                    throw new Exception("Answer cant be empty");
                }
                else
                {
                    Change(choice, user);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void Change(string answ, BankUser user)
        {
            try
            {
                switch (answ)
                {
                    case "1":
                        {
                            ChangePhoneNumber(user);
                            break;
                        }
                    case "2":
                        {
                            ChangePassword(user);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Invalid choice");
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        ///==================================================================== [  CHANGE NUMBER ] ========================
        private static void ChangePhoneNumber(BankUser user)//изменени пароля
        {
            Console.Write($"Current phone number:");
            user.PhoneNumber.ShowHiddenNumber();//показывется скрыто для подтверждения пользователем
            try
            {

                Console.Write(@"Are you shure? [y]\[n]: ");
                string answ;
                if (!string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    switch (answ)
                    {
                        case "y":
                            {
                                ConfirmPhoneNumber(user);

                                break;
                            }
                        case "n":
                            {
                                Console.WriteLine("Return....");
                                break;
                            }
                        default:
                            throw new Exception("Invalid answer");


                    }
                }
                else
                {
                    throw new Exception("");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void ConfirmPhoneNumber(BankUser user)// метод для подтв.номера
        {
            Console.Write("Enter full current number: ");
            string userNumber;
            try
            {
                if (string.IsNullOrEmpty(userNumber = Console.ReadLine()))
                {
                    throw new Exception("You enter empty number");
                }
                else
                {
                    if (CompareNumbers(userNumber, user)) //сравнивает ввел ли пользователь с=корректный намб
                    {
                        CreateNewPhoneNumber(user);//изменениее
                    }
                    else
                    {
                        throw new Exception("You enter incorrect number");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void CreateNewPhoneNumber(BankUser user)//для  изменения номера если пользователь ввел правильный номер
        {
            Console.Write("Enter new phone number: ");
            try
            {

                if (string.IsNullOrEmpty(user.PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("You enter empty number");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static bool CompareNumbers(string number, BankUser user)//сравнивает номера для проверки в ConfirmPhoneNumber
        {
            return number == user.PhoneNumber;
        }

        private static void ShowHiddenNumber(this string number) //показывает номер в виде: +380*******000 для подтверждения 
        {

            for (int i = 0; i < number.Length; i++)
            {
                if (i >= 4 && i <= number.Length - 4)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(number[i]);
                }
            }
            Console.WriteLine();
        }

        //=================================================================================

        //=================================================================== [ CHANGE PASSWORD ] =======================================


        //TODO:ПРОЕБАЖИТь
        private static void ChangePassword(BankUser user)
        {
            Console.WriteLine(@"Are you sure to want change password? : [y]\[n] ");
            string? answ;
            try
            {
                if (string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    throw new Exception("Empty answer");
                }
                else
                {
                    switch (answ)
                    {
                        case "y":
                            {
                                ConfirmPassword(user);
                                break;
                            }
                        case "n":
                            {
                                Console.WriteLine("Return...");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Invalid answer");
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void ConfirmPassword(BankUser user)
        {
            Console.Write("Enter your password: ");
            try
            {
                string? pass;
                if (string.IsNullOrEmpty(pass = Console.ReadLine()))
                {
                    throw new Exception("Pass cant be empty");
                }
                else
                {
                    if (ComparePass(pass, user))
                    {
                        CreateNewPassword(user);
                    }
                    else
                    {
                        throw new Exception("Invalid password");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static bool ComparePass(string pass, BankUser user)
        {
            return pass == user.Password;
        }

        private static void CreateNewPassword(BankUser user)
        {
            Console.Write("Enter new password: ");
            if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
            {
                throw new Exception("Pass cant be empty");
            }
        }
    }
}
