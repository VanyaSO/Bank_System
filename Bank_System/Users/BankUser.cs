using System;
using System.Collections.Generic;
using System.Linq;
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

        public override string ToString()
        {
            return $"ID:{ID}\n{base.ToString()}\nДата рождения: {BDate.Date.ToShortDateString()}\nНомер телефона: {PhoneNumber}";
        }



    }
}
