using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal static class AdminInterface
    {
        public static void ChangeAdmin(this Admin adm) //Изменение пароля
        {
            string? answ;
            Console.Write(@"Вы уверенный что хотите сменить пароль?: [д]\[н] ");
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
                                ConfirmPass(adm);
                                break;
                            }
                        case "н":
                            {
                                Console.Write("Возвращение..");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Некорректный выбор: ");
                            }
                    }
                    ConfirmPass(adm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ConfirmPass(Admin adm)
        {
            Console.Write("Введите свой текущий пароль: ");
            string answPass;
            if(string.IsNullOrEmpty(answPass = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                if (ComparePass(answPass, adm))
                {
                    ChangePass(adm);
                }
                else
                {
                    throw new Exception("Введен неверный пароль");
                }
                
            }

        }
        private static void ChangePass(Admin adm)
        {
            Console.Write("Введите новый пароль: ");
            string? newPass;
            if (string.IsNullOrEmpty(newPass = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                adm.Password = newPass;
            }

        }

        private static bool ComparePass(string pass,Admin adm)
        {
            return adm.Password == pass;
        }



       
    }
}
