using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal class Admin : MainUser
    {
        public Admin() : base() { }

        public Admin(string name, string login, string pass) : base(name, login, pass) { }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
