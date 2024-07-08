
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

        public bool ComparePass(string pass) => pass == Password;

        public void ChangePass(string pass) => Password = pass;
        public void ChangeLogin(string login) => Login = login;
        

        

       

        //public void ChangeAdminPass()
        //{
        //    string? answ;
        //    Console.WriteLine("Введите текущий пароль: ");
        //    if(string.IsNullOrEmpty(answ = Console.ReadLine())){
        //        throw new Exception("Вы ввели пустую строку");
        //    }
        //    else
        //    {
        //        if (ComparePass(answ))
        //        {
        //            ChangePass();
        //        }
        //        else
        //        {
        //            throw new Exception("Некорректный пароль");
        //        }
        //    }

        //}

       
        //private void ChangePass()
        //{
        //    Console.Write("Введите новый пароль: ");
        //    string? newPass;
        //    if (string.IsNullOrEmpty(newPass = Console.ReadLine()))
        //    {
        //        throw new Exception("Вы ввели пустую строку");
        //    }
        //    else
        //    {
        //        Password = newPass;
        //    }
        //}


        

        




    }
}
