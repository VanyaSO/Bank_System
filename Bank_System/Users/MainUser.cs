
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal abstract class MainUser
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

        

        public static MainUser? LogIn(List<MainUser> users)
        {

            string pass, login;
            Console.Write("Введите логин: ");
            if(string.IsNullOrEmpty(login = Console.ReadLine()))
            {
                throw new Exception("Пустое поле логина");
            }

            if (IsRegistered(login, users))
            {
            
                Console.Write("Введите пароль: ");
                if(string.IsNullOrEmpty(pass = Console.ReadLine()))
                {
                    throw new Exception("Пустое поле пароля");
                }

                MainUser? user = EnterInAccount(login, pass, users);

                return user;

            }
            else
            {
                throw new Exception("Аккаунт не с таким логином не найден");
            }

            

            
        }
       
        public static MainUser? EnterInAccount(string login,string pass,List<MainUser?> users)
        {
            foreach(var user in users)
            {
                if(user.Login == login)
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
                else
                {
                    throw new Exception("Неправильный логин");
                }
            }

            return null;
        } 
        
        private static bool IsRegistered(string login,List<MainUser> users) 
        {
            foreach(var el in users)
            {
                if(el.Login == login)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"Имя: {Name}\nЛогин: {Login}\nПароль: {Password}";
        }
    }
}
