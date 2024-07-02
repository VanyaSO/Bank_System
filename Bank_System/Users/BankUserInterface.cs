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

                Console.Write("Введите Ваше имя: ");
                if (string.IsNullOrEmpty(user.FullName = Console.ReadLine())) //проверка на пустую строку
                {
                    throw new Exception("Имя не может быть пустым");
                }

                Console.Write("Введите Ваш логин: ");
                if (string.IsNullOrEmpty(user.Login = Console.ReadLine()))
                {
                    throw new Exception("Логин не может быть пустым");
                }

                Console.Write("Создайте новый пароль: ");

                if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
                {
                    throw new Exception("Пароль не может быть пустым");
                }

                Console.Write("Введите дату Вашего рождения: ");
                string date;
                if(string.IsNullOrEmpty(date = Console.ReadLine()))
                {
                    throw new Exception("Дата рождения не может бюьб пустой");
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

                Console.Write("Введите PassID [или нажмите [Enter] для автоматического создания ID]: ");
                if (string.IsNullOrEmpty(user.ID = Console.ReadLine()))
                {
                    user.ID = createNewId();
                }


                Console.Write("Введите Ваш номер телефона: ");
                if (string.IsNullOrEmpty(user.PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Телефон не может быть пустым");

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

            Console.WriteLine("1 - Изменить номер телефона\t2 - Изменить пароль");
            string choice;
            try
            {

                if (string.IsNullOrEmpty(choice = Console.ReadLine()))
                {
                    throw new Exception("Выбор не может быть пустым");
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
                            throw new Exception("Некорректный выбор");
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
            Console.Write($"Текущий номер телефона:");
            user.PhoneNumber.ShowHiddenNumber();//показывется скрыто для подтверждения пользователем
            try
            {

                Console.Write(@"Вы уверены? [д]\[н]: ");
                string answ;
                if (!string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    switch (answ)
                    {
                        case "д":
                            {
                                ConfirmPhoneNumber(user);

                                break;
                            }
                        case "н":
                            {
                                Console.WriteLine("Возвращем....");
                                break;
                            }
                        default:
                            throw new Exception("Некорректный выбор");


                    }
                }
                else
                {
                    throw new Exception("Ответ не может быть пустым");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void ConfirmPhoneNumber(BankUser user)// метод для подтв.номера
        {
            Console.Write("Введите текущий номер телефона: ");
            string userNumber;
            try
            {
                if (string.IsNullOrEmpty(userNumber = Console.ReadLine()))
                {
                    throw new Exception("Поле не может быть пустым");
                }
                else
                {
                    if (CompareNumbers(userNumber, user)) //сравнивает ввел ли пользователь с=корректный намб
                    {
                        CreateNewPhoneNumber(user);//изменениее
                    }
                    else
                    {
                        throw new Exception("Вы ввели некоректный номер");
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
            Console.Write("Введите новый номер телефона: ");
            try
            {

                if (string.IsNullOrEmpty(user.PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Поле не может быть пустым");
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
            Console.WriteLine(@"Вы действительно желаете сменить пароль? : [д]\[н] ");
            string? answ;
            try
            {
                if (string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    throw new Exception("Некорректный ответ");
                }
                else
                {
                    switch (answ)
                    {
                        case "д":
                            {
                                ConfirmPassword(user);
                                break;
                            }
                        case "н":
                            {
                                Console.WriteLine("Возвращаем...");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Некорректный ответ");
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
            Console.Write("Введите текущий пароль: ");
            try
            {
                string? pass;
                if (string.IsNullOrEmpty(pass = Console.ReadLine()))
                {
                    throw new Exception("Пароль не может быть пустым");
                }
                else
                {
                    if (ComparePass(pass, user))
                    {
                        CreateNewPassword(user);
                    }
                    else
                    {
                        throw new Exception("Некорректный пароль");
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
            Console.Write("Введите новый пароль: ");
            if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
            {
                throw new Exception("Пароль не может быть пустым");
            }
        }
    }
}
