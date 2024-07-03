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

        public MainUser() { }
        public MainUser(string name,string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }




        public override string ToString()
        {
            return $"Имя: {Name}\nЛогин: {Login}\nПароль: {Password}";
        }
    }
}
