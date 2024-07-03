using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal class Admin : MainUser
    {
        public Admin() : base() { }

        public Admin(string name, string login, string pass) : base(name, login, pass) { }



        public void ChangeAdmin()
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
                                ConfirmPass(this);
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
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ConfirmPass(Admin adm)
        {
            Console.Write("Введите свой текущий пароль: ");
            string answPass;
            if (string.IsNullOrEmpty(answPass = Console.ReadLine()))
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
        private void ChangePass(Admin adm)
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

        private bool ComparePass(string pass,Admin adm)
        {
            return pass == adm.Password;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
