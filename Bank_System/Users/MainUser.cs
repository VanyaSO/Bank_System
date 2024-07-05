
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    public abstract class MainUser
    {
        public string Name { get; set; } //не меняется 
        public string Login { get; set; } //не меняется
        public string Password { get; set; }

        public Role UserRole { get; private set; }

        public MainUser() { }
        public MainUser(string name,string login, string password, Role userRole)
        {
            Name = name;
            Login = login;
            Password = password;
            UserRole = userRole;
        }




        public static void LogIn()
        {

            string pass, login;
            Console.Write("Введите логин: ");
            if(string.IsNullOrEmpty(login = Console.ReadLine()))
            {
                throw new Exception("Пустое поле логина");
            }

            if (IsRegistered(login))
            {
            
                Console.Write("Введите пароль: ");
                if(string.IsNullOrEmpty(pass = Console.ReadLine()))
                {
                    throw new Exception("Пустое поле пароля");
                }

                Common.User = EnterInAccount(login, pass);

                

            }
            else
            {
                throw new Exception("Аккаунт с таким логином не найден");
            }

            

            
        }
       
        public static MainUser? EnterInAccount(string login,string pass)
        {
            foreach(var user in Common.CurrentBank.Users) //из пользователей банка который выберет
            {
                if (user.Login == login)
                {

                    if(user.Password == pass)
                        {
                          return user;
                    }
                    else
                    {
                        throw new Exception("Неправлильный пароль");
                    }
                }
                
            }

            throw new AccessViolationException("Аккаунт с таким логином не найден");
        } 
        

       

        private static bool ConfirmPass(MainUser? user)
        {
            Console.Write("Введите пароль: ");
            string pass = Console.ReadLine();
            if(user.Password == pass)
            {
                return true;
            }
            return false;
            
        }

        private static bool IsRegistered(string login) 
        {
            foreach(var el in Common.CurrentBank.Users)
            {
                if(el.Login == login)
                {
                    return true;
                }
            }
            throw new AccessViolationException("Аккаунт с таким логином не найден");
        }

        public override string ToString()
        {
            return $"Имя: {Name}\nЛогин: {Login}\nПароль: {Password}";
        }
    }
}
