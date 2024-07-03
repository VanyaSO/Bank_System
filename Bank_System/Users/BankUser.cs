using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Bank_System
{
    internal class BankUser : MainUser
    {
        

        public DateTime BDate { get; set; } // не меняется 
        public string PhoneNumber { get; set; } // меняетя 
        public string ID { get; set; } // не меняется 

        //List<> list { get; set; }


        public BankUser() : base() { }
        public BankUser(string name, string login, string pass, string phoneNumb, string id, DateTime date) : base(name,login, pass)
        {
            
            BDate = date;
            PhoneNumber = phoneNumb;
            ID = id;

        }


        public void Registration()
        {
            try
            {

                Console.Write("Введите Ваше имя: ");

                if (string.IsNullOrEmpty(Name = Console.ReadLine())) 
                {
                    throw new Exception("Имя не может быть пустым");
                }


                Console.Write("Введите Ваш логин: ");
                if (string.IsNullOrEmpty(Login = Console.ReadLine()))
                {
                    throw new Exception("Логин не может быть пустым");
                }

                Console.Write("Создайте новый пароль: ");

                if (string.IsNullOrEmpty(Password = Console.ReadLine()))
                {
                    throw new Exception("Пароль не может быть пустым");
                }

                Console.Write("Введите дату Вашего рождения: ");
                string? date;
                if (string.IsNullOrEmpty(date = Console.ReadLine()))
                {
                    throw new Exception("Дата рождения не может бюьб пустой");
                }
                else
                {

                    string[] parts = date.Split(' ');
                    int day = Convert.ToInt16(parts[0]);
                    int month = Convert.ToInt16(parts[1]);
                    int year = Convert.ToInt16(parts[2]);


                    BDate = new DateTime(year, month, day);
                }


                Console.Write("Введите PassID [или нажмите [Enter] для автоматического создания ID]: ");
                if (string.IsNullOrEmpty(ID = Console.ReadLine()))
                {
                    ID = CreateNewId();
                }


                Console.Write("Введите Ваш номер телефона: ");
                if (string.IsNullOrEmpty(PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Телефон не может быть пустым");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private string CreateNewId()
        {
            Random rand = new Random();
            return Convert.ToString(rand.Next(1000,9999));
        }

        public void ChangeUser()
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
                    Change(choice, this);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void Change(string answ,BankUser user)
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
        private void ChangePhoneNumber(BankUser user)
        {
            Console.Write($"Текущий номер телефона:");
            ShowHiddenNumber(user.PhoneNumber);//показывется скрыто для подтверждения пользователем
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
        private void ConfirmPhoneNumber(BankUser user)
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

        private void CreateNewPhoneNumber(BankUser user) //изменение после проверки
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
        
        private bool CompareNumbers(string number, BankUser user) //сравнивание номеров
        {
            return number == user.PhoneNumber;
        }
        private void ShowHiddenNumber(string number) 
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

        public override string ToString()
        {
            return $"ID:{ID}\n{base.ToString()}\nДата рождения: {BDate.Date.ToShortDateString()}\nНомер телефона: {PhoneNumber}";
        }


        private void ChangePassword(BankUser user)
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


        private void ConfirmPassword(BankUser user)
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

        public bool ComparePass(string pass, BankUser user)
        {
            return pass == user.Password;
        }

        private void CreateNewPassword(BankUser user)
        {
            Console.Write("Введите новый пароль: ");
            if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
            {
                throw new Exception("Пароль не может быть пустым");
            }
        }
    }

    
}
