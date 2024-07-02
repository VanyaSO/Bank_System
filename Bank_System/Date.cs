using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank_System
{
    internal struct Date
    {
        private short Day { get; set; }
        private short Month { get; set; }
        private short Year { get; set; }

        

        public Date(short day,short month,short year)
        {
            Day = day;
            Month = month;  
            Year = year;

            
        }
        
        public override string ToString()
        {
            return $"{Day},{Month},{Year}";
        }

        private bool isValidDay(short day) //проверка корректости дня
        {
            return day > 0 && day <= 31 ;
        }

        private bool IsValidMonth(short month) //корректность месяца
        {
            return month > 0 && month <= 12 ;
        }

        private bool IsValidYear(short year)
        {

            return year > 1904 && year <= DateTime.Now.Year ; //максимальный возраст пользователя = 120
        }
        
        public bool IsValidDate(short day,short month,short year) { 
            
            if(isValidDay(day) && IsValidMonth(month)&& IsValidYear(year)) {
                return true;
            }
            return false;
        }
        

        
        public void CreateDate() //заполение и проверка на валидацию текущего обькета даты,по кд он заполняется {0,0,0}
        {
            Console.Write("Enter date: [Exmpl: 01 01 2000] ");
            string inputDate = Console.ReadLine();

            string[] partsOfDate = inputDate.Split(' ');
            try
            {

                if(partsOfDate.Length == 3)
                {
                    short day = Convert.ToInt16(partsOfDate[0]);
                    short month = Convert.ToInt16(partsOfDate[1]);
                    short year = Convert.ToInt16(partsOfDate[2]);   

                    if (IsValidDate(day, month,year))
                    {
                        Day = day;
                        Month = month;
                        Year = year;

                    }
                    else
                    {
                        throw new Exception("Invalid date");
                    }

                }
                else
                {
                    throw new Exception("Invalid date");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
           
        }


    }
}
