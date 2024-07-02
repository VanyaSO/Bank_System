using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal abstract class MainUser
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public MainUser() { }
        public MainUser(string login, string password)
        {
            Login = login;
            Password = password;
        }

        

        public override string ToString()
        {
            return $"Login: {Login}\nPassword: {Password}";
        }
    }
}
