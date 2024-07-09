
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace Bank_System
{
    internal class BankUser : MainUser
    {


        private readonly DateOnly BDate; // не меняется 
        public string PhoneNumber { get; set; } // меняетя 
        private string ID { get; set; } // не меняется 

        public List<Card> UserCards { get; set; }


        public BankUser() : base() { UserCards = new List<Card>() {}; }
        public BankUser(string name, string login, string pass, string phoneNumb, string id, DateOnly date,List<Card> userCards) : base(name,login, pass,Role.BankUser)
        {
            
            BDate = date;
            PhoneNumber = phoneNumb;
            ID = id;
            UserCards = userCards;
        }
        public BankUser(string name, string login, string pass, string phoneNumb, string id, DateOnly date) : base(name,login, pass,Role.BankUser)
        {
            
            BDate = date;
            PhoneNumber = phoneNumb;
            ID = id;
            UserCards = new List<Card> ();
        }


        public void Registration()
        {
            try
            {

                Console.Write("Введите Ваше имя: ");

                if (string.IsNullOrEmpty(Name = Console.ReadLine())) 
                {
                    throw new Exception("Имя не может быть пустым");
                }


                Console.Write("Введите Ваш логин: ");
                if (string.IsNullOrEmpty(Login = Console.ReadLine()))
                {
                    throw new Exception("Логин не может быть пустым");
                }

                Console.Write("Создайте новый пароль: ");

                if (string.IsNullOrEmpty(Password = Console.ReadLine()))
                {
                    throw new Exception("Пароль не может быть пустым");
                }

                Console.Write("Ввелитте дату рождения");

                DateOnly dateB = DateOnly.Parse(Console.ReadLine());
                if ((DateTime.Today.Year - dateB.Year) <= 18)
                {
                  throw new Exception("Ваш возраст меньше 18");  //добавить проверку на день
                }
              


                Console.Write("Введите PassID [или нажмите [Enter] для автоматического создания ID]: ");
                if (string.IsNullOrEmpty(ID = Console.ReadLine()))
                {
                    ID = CreateNewId();
                }


                Console.Write("Введите Ваш номер телефона: ");
                if (string.IsNullOrEmpty(PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Телефон не может быть пустым");

                }


                Console.WriteLine("По умолчанию валюта новой карты: UAH");
                
                string newPin;
                Console.Write("Введите новый PIN(4 цифры): ");
                if(string.IsNullOrEmpty(newPin = Console.ReadLine()) || newPin.Length < 4)
                {
                    throw new Exception("Новый PIN для карты не может быть пустым");
                }
                else 
                {
                    OpenNewCard(newPin,CurrencyType.UAH);

                    Common.Bank.Users.Add(this);
                
                }
            }
            catch (Exception ex)
            {
                Message.ErrorMessage(ex.Message);
            }
        }

        private string CreateNewId()
        {
            Random rand = new Random();
            return Convert.ToString(rand.Next(1000,9999));
        }

        //Показать все карты

        public void ShowAllCards()
        {
            foreach (var el in UserCards)
            {
                Console.WriteLine(el);
            }
        }
     

        //Измненить User
        public void ChangePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
   
        public void ChangePassword(string password) => Password = password;

        public bool ComparePass(string pass) => pass == Password;
        public bool CompareNumbers(string number) => number == PhoneNumber; 
        


        public void ShowHiddenNumber(string number)  //для проверки юзера
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (i >= 4 && i <= number.Length - 4)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(number[i]);
                }
            }
            Console.WriteLine();
        }

        
        
        ///
        public bool ComparePin(Card card) //сравнивает и блочит карту после 3х неверных попыток
        {
            int count = 0;
            do
            {
                

                Console.Write("Введите PIN: ");
                string? pin;
                if (string.IsNullOrEmpty(pin = Console.ReadLine()))
                {
                    throw new Exception("Вы ввели пустую строку");
                }
                else
                {
                    if (card.VerifyPinCode(pin))
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Неправильный PIN");
                        Console.WriteLine($"До блокировки карты осталось: {count - 3}");//Протестить
                    }
                    
                }

                count++;
                
            } while (count!=3);
            return false;
        }

       

        //ПЕРЕДАЧА ДЕНЕГ

        public void SendMoney()
        {
            Console.WriteLine("Активные карты: ");
            ShowAllActiveCards();
            try
            {

                string? sCardNumber;
                if (string.IsNullOrEmpty(sCardNumber = Console.ReadLine())){
                    throw new Exception("Вы ввели пустую строку");
                }
                else
                {
                    //доделать туду ниже
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Card? GetCardByNumber(string number) //возвращает карту по номеру
        {
            foreach (Card card in UserCards)
            {
                if(card.CardNumber == number)
                {
                    return card;
                }
            }
            throw new Exception("Карта не найдена");
        }

        private void SendMoneyFromChoosenCard(Card card)
        {
            try
            {
                if (IsActive(card))
                {
                    //Доделать ниже туду
                }
                else
                {
                    throw new Exception("Выбранная карта заблокирована");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private bool IsActive(Card card) //активна ли карта
        {
            return card.Status == CardStatus.Active;
        }


        public void ShowAllActiveCards() //карты которые активные
        {
            foreach(Card card in UserCards)
            {
                if (IsActive(card))
                {
                    Console.WriteLine("==============");
                    Console.WriteLine($"{card.CardNumber} : {card.Balance}{card.Currency}") ;
                    Console.WriteLine("==============");
                }
            }
        }

        //TODO: max(доделать когда в Bank будет массив юзеров)искать по номеру карты/имени

        


        public override string ToString()
        {
            return $"ID:{ID}\n{base.ToString()}\nДата рождения: {BDate.ToString()}\nНомер телефона: {PhoneNumber}";
        }

      
        public void OpenNewCard(string pin,CurrencyType currency)
        {
             Card newCard = new Card(pin, currency);
             AddNewCard(newCard);
        }

  


        public void ShowAvailibleCurrency()//показывает доступные валюты длч открытия
        {
            foreach(CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
            {
                if (!HaveUserCurrency(currency))
                {
                    Console.Write($"{currency} ") ;
                }
            }
            Console.WriteLine();
        }

        public bool HaveUserCurrency(CurrencyType currency)//проверка на существование открытой карты с такой же валютой
        {
            
            foreach(Card card in UserCards)
            {
                if(currency == card.Currency)
                {
                    return true;
                }
            }

            return false;
        }




        public bool CanUserOpenNewCard() //может ли открыть карту новую
        {
            foreach (CurrencyType cur in Enum.GetValues(typeof(CurrencyType)))
            {
                if (!HaveUserCurrency(cur))
                {
                    return true;
                }
            }
            return false;
        }
        private void AddNewCard(Card newCard)
        {
            UserCards.Add(newCard);
        }

        


        ///БЛОК КАРТЫ 
       
        public void BlockCard(string number)
        {
            
                
            if(!string.IsNullOrEmpty(number))
            {
                Card? blockCard = GetCardByNumber(number);
                if(blockCard != null)
                {
                    blockCard.BlockCard();
                    Message.SuccessMessage($"Карта {number} успешно заблокирована");
                }
                else
                {
                    throw new Exception("Карта с таким номером не найдена");
                }

            }
            else
            {
                throw new Exception("Поле строки не может быть пустым");

            }
            
        }


        

        public void ShowSendedTransactionByCard(List<Transaction> list)
        {
            if(list.Count > 0)
            {

                foreach (var el in list)
                {
                    if(el.RecipientName == Common.User.Name)
                    {
                        Console.WriteLine(el);
                    }
                }
            }
            else
            {
                throw new Exception("Нет доступных транзакций");
            }
        }


        public void ShowCompleteTransactionByCard(List<Transaction> list)
        {
            if(list.Count > 0)
            {

                foreach(var el in list)
                {
                    if(el.SenderInitials == Common.User.Name)
                    {
                        Console.WriteLine(el);

                    }
                }
            }
            else
            {
                throw new Exception("Нет доступных транзакций");
            }
        }
     
        public void ShowCompleteSum(Card card)
        {
            decimal localSum = 0;

            foreach(Transaction transaction in card.GetAllTransactions())
            {
                if(transaction.RecipientName == Name)
                {
                    localSum += transaction.Amount;
                }


            }

            Console.WriteLine($"{card.CardNumber}: {localSum} [{card.Currency}]");
            
        }


        public void ShowSendedSum(Card card)
        {
            

            decimal localSum = 0;

            foreach(Transaction transaction in card.GetAllTransactions())
            {
                if(transaction.SenderInitials == Name) 
                {
                    localSum += transaction.Amount;
                }
            }

            Console.WriteLine($"{card.CardNumber}: {localSum} [{card.Currency}]");
           
            
            
        }





        public void SendMoney(Card card,Card cardForTranf)
        {
            string sumForSend;
            if (string.IsNullOrEmpty(sumForSend = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                decimal resultSum = Int32.Parse(sumForSend);
                // card.Transfer(cardForTranf, resultSum, card.Currency, Name);
                Message.SuccessMessage("Перевод успешно выполнен");
            }
        }

        public Card? GetCardForTransfer(string number)
        {

            List<BankUser> users = GetListUsers();

            foreach(var el in users) {
               foreach(Card card in el.UserCards)
               {
                    if(card.CardNumber == number)
                    {
                        return card;
                    }
               }
            }
            throw new Exception("Карта не найдена");
        }

        private List<BankUser> GetListUsers()
        {
            List<BankUser> users = new List<BankUser>();

            foreach(var user in Common.Bank.Users) {
                if(user.UserRole == Role.BankUser)
                {
                    users.Add(user as BankUser);
                }
            }

            return users;
        }

        //ТРАНЗАКЦИИ
        //public void MyTransaction()
        //{

        //    MyTransactionMenu();
        //    int action = MainMenu.GetActionMenu(1, 2);

        //    switch(action)
        //    {
        //        case 1:
        //        {
        //             ShowMySendedTransaction();
        //             break;
        //        }
        //        case 2:
        //        {
        //            ShowCompletedTransaction();
        //            break;
        //        }
        //    }

        //}



        //private void MyTransactionMenu() //TODO:перенести в ЮзерМеню
        //{
        //    Console.WriteLine("1) Показать все отправленые транзакции");
        //    Console.WriteLine("2) Показать все принятые транзакции");
      
        //    Console.WriteLine("----");
        //    Console.WriteLine("Введите ответ: ");
        //}

        /////ТРАНЗАКЦИИ ОТПРАВЛЕННЫЕ??? перенеси в Personenu();
        //private void ShowMySendedTransaction()
        //{
        //    foreach(Card card in UserCards)
        //    {
        //        ShowSendTransByCard(card.GetAllTransactions());
        //    }
        //}


        //private void ShowSendTransByCard(List<Transaction> cardTransaction) //конкретной карты
        //{
        //    foreach(Transaction transaction in cardTransaction)
        //    {
        //        if(IsSendByMe(transaction))
        //        {
        //            Console.WriteLine(transaction.ToString());
        //        }
        //    }
        //}

        //private bool IsSendByMe(Transaction trans) //пользователем ли отправлена?
        //{
        //    return trans.SenderInitials == Name;
        //}
        
        ////ТРАНЗАКЦИИ ПРИНЯТЫЕ


        //private void ShowCompletedTransaction() //общая для получатель(Мы)
        //{
        //    foreach(Card card in UserCards)
        //    {
        //        ShowCompletedTransactionByCard(card.GetAllTransactions());
        //    }
        //}

        //private void ShowCompletedTransactionByCard(List<Transaction> cardTransaction) //по каждой карте траназкция в которой мы получатель
        //{
        //    foreach(Transaction transaction in cardTransaction)
        //    {
        //        if (IsCompleted(transaction))
        //        {
        //            Console.WriteLine(transaction.ToString());
        //        }
        //    }
        //}
        
        //private bool IsCompleted(Transaction transaction) //являемся ли мы получателем
        //{
        //    return transaction.RecipientName == Name;
        //}

        //СУММА ОТПРАВЛЕННЫХ

        //TODO:остановился тут 15:55
    }




    
}
