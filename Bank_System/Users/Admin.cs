
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

        public Admin(string name, string login, string pass) : base(name, login, pass, Role.Admin) { }


        public void ShowUsersInfo(List<MainUser> users)
        {
            foreach(var user in users)
            {

                Console.WriteLine(user); //TODO: изменить ToString();
            }
        }


        public void ChangeAdminPass()
        {
            string? answ;
            Console.WriteLine("Введите текущий пароль: ");
            if(string.IsNullOrEmpty(answ = Console.ReadLine())){
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                if (ComparePass(answ))
                {
                    ChangePass();
                }
                else
                {
                    throw new Exception("Некорректный пароль");
                }
            }

        }


        //public void ChangeAdmin()
        //{
        //    string? answ;
        //    Console.Write(@"Вы уверенный что хотите сменить пароль?: [д]\[н] ");
        //    try
        //    {

        //        if (string.IsNullOrEmpty(answ = Console.ReadLine()))
        //        {
        //            throw new Exception("Некорректный ответ");
        //        }
        //        else
        //        {
        //            switch (answ)
        //            {
        //                case "д":
        //                    {
        //                        ConfirmPass(this);
        //                        break;
        //                    }
        //                case "н":
        //                    {
        //                        Console.Write("Возвращение..");
        //                        break;
        //                    }
        //                default:
        //                    {
        //                        throw new Exception("Некорректный выбор: ");
        //                    }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

       
        private void ChangePass()
        {
            Console.Write("Введите новый пароль: ");
            string? newPass;
            if (string.IsNullOrEmpty(newPass = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                Password = newPass;
            }
        }

        private bool ComparePass(string pass)
        {
            return pass == Password;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        //public MainUser? Loginin(string login, string password, List<MainUser> users)
        //{
        //    foreach (var account in users)
        //    {

        //        if (account.Login == login && account.Password == password)
        //        {
        //            return account;
        //        }
        //    }
        //    return null;
        //}




    }
}
