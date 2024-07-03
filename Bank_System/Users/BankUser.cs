using Bank_System.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace Bank_System
{
    internal class BankUser : MainUser
    {
        

        private  DateTime BDate { get; set; } // не меняется 
        public string PhoneNumber { get; set; } // меняетя 
        private string ID { get; set; } // не меняется 

        private List<Card> UserCards { get; set; }


        public BankUser() : base() { UserCards = new List<Card>(); }
        public BankUser(string name, string login, string pass, string phoneNumb, string id, DateTime date,List<Card> userCards) : base(name,login, pass)
        {
            
            BDate = date;
            PhoneNumber = phoneNumb;
            ID = id;
            UserCards = userCards;
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

                Console.Write("Введите дату Вашего рождения: ");
                string? date;
                if (string.IsNullOrEmpty(date = Console.ReadLine()))
                {
                    throw new Exception("Дата рождения не может бюьб пустой");
                }
                else
                {

                    string[] parts = date.Split(' ');
                    int day = Convert.ToInt16(parts[0]);
                    int month = Convert.ToInt16(parts[1]);
                    int year = Convert.ToInt16(parts[2]);


                    BDate = new DateTime(year, month, day);
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

                Card newCard = new Card("0000", CurrencyType.UAH);
                Console.WriteLine("По умолчанию PIN - 0000 Валюта: UAH");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private string CreateNewId()
        {
            Random rand = new Random();
            return Convert.ToString(rand.Next(1000,9999));
        }

        //Подтверждение PIN

        

        //ПОПОЛНЕНИЕ КАРТЫ

        public void ChooseCardForAddMoney() //общий метод для ввода
        {
            Console.WriteLine("Активные карты: ");
            ShowAllActiveCardNumbers();
            try
            {
                string sCardNumber;
                if(string.IsNullOrEmpty(sCardNumber = Console.ReadLine()))
                {
                    throw new Exception("Вы ввели пустую строку");
                }
                else
                {
                    Card? card = GetCardByNumber(sCardNumber);
                    if (ComparePin(card))
                    {
                        DepositMoneyOnBalance(GetCardByNumber(sCardNumber));

                    }
                    else
                    {
                        Console.WriteLine("Вы превысили лимит попыток ввода PIN\nВаша карта будет заблокирована");
                        card.BlockCard();
                    }

                   
                   
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        

        private bool ComparePin(Card card) //сравнивает и блочит карту после 3х неверных попыток
        {
            int count = 0;
            do
            {
                

                Console.Write("Введите PIN: ");
                string pin;
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

        private void DepositMoneyOnBalance(Card? card) //внос денег
        {
            Console.WriteLine($"Текущий баланс: {card.Balance} [{card.Balance.ToString()}]");
            Console.Write("Введите сумму для зачисления: ");
            string inputSum;
            
            if(string.IsNullOrEmpty(inputSum = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                if (IsCorrectSum(decimal.Parse(inputSum))) 
                {
                    card.Deposit(decimal.Parse(inputSum));

                }
                

            }
        }

        private bool IsCorrectSum(decimal sum)
        {
            return sum > 0;
        }


        //СНЯТИЕ ДЕНЕГ

        public void ChooseCardForWitdraw()
        {
            Console.Write("Активные карты: ");
            ShowAllActiveCardNumbers();
            string sCardNumber;
            if(string.IsNullOrEmpty(sCardNumber = Console.ReadLine()))
            {
                throw new Exception("Вы ввели некорректный номер");
            }
            else
            {
                Card? card = GetCardByNumber(sCardNumber);
                if (ComparePin(card))
                {
                    WithdrawMoney(card);
                }
                else
                {
                    card.BlockCard();
                }
            }
        }


        private void WithdrawMoney(Card card)
        {

            Console.WriteLine($"Текущий баланс: {card.Balance} [{card.Currency.ToString()}]");
            Console.Write("Введите сумму для снятия: ");
            string inputSum;
            if(string.IsNullOrEmpty(inputSum = Console.ReadLine()))
            {
                throw new Exception("Вы ввели пустую строку");
            }
            else
            {
                if (IsCorrectSum(decimal.Parse(inputSum)) && HaveEnoughMoney(card,decimal.Parse(inputSum))) //если сумма корректна и хватает денег
                {
                    card.Withdraw(decimal.Parse(inputSum));

                }
            }
        }

        private bool HaveEnoughMoney(Card card,decimal sum) {
            return card.Balance >= sum;
        }

        //

        //ПЕРЕДАЧА ДЕНЕГ

        public void SendMoney()
        {
            Console.WriteLine("Активные карты: ");
            ShowAllActiveCardNumbers();
            try
            {

                string sCardNumber;
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

        private Card? GetCardByNumber(string number) //возвращает карту по номеру
        {
            foreach (Card card in UserCards)
            {
                if(card.CardNumber == number)
                {
                    return card;
                }
            }
            return null;
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


        private void ShowAllActiveCardNumbers() //номера карт которые активные
        {
            foreach(Card card in UserCards)
            {
                if (IsActive(card))
                {
                    Console.WriteLine(card.CardNumber);
                }
            }
        }

        //TODO: max(доделать когда в Bank будет массив юзеров)искать по номеру карты/имени

        //

        public void ChangeUser()//общая, для выбора
        {
            Console.WriteLine("1 - Изменить номер телефона\t2 - Изменить пароль");
            string choice;
            try
            {

                if (string.IsNullOrEmpty(choice = Console.ReadLine()))
                {
                    throw new Exception("Выбор не может быть пустым");
                }
                else
                {
                    Change(choice, this);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //ИЗМЕНЕНИЕ ПОЛЬЗОВАТЕЛЯ

        private void Change(string answ,BankUser user)
        {
            try
            {
                switch (answ)
                {
                    case "1":
                        {
                            ChangePhoneNumber(user);
                            break;
                        }
                    case "2":
                        {
                            ChangePassword(user);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Некорректный выбор");
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        //ТЕЛЕФОН
        private void ChangePhoneNumber(BankUser user)
        {
            Console.Write($"Текущий номер телефона:");
            ShowHiddenNumber(user.PhoneNumber);//показывется скрыто для подтверждения пользователем
            try
            {

                Console.Write(@"Вы уверены? [д]\[н]: ");
                string answ;
                if (!string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    switch (answ)
                    {
                        case "д":
                            {
                                ConfirmPhoneNumber(user);

                                break;
                            }
                        case "н":
                            {
                                Console.WriteLine("Возвращем....");
                                break;
                            }
                        default:
                            throw new Exception("Некорректный выбор");


                    }
                }
                else
                {
                    throw new Exception("Ответ не может быть пустым");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void ConfirmPhoneNumber(BankUser user)
        {
            Console.Write("Введите текущий номер телефона: ");
            string userNumber;
            try
            {
                if (string.IsNullOrEmpty(userNumber = Console.ReadLine()))
                {
                    throw new Exception("Поле не может быть пустым");
                }
                else
                {
                    if (CompareNumbers(userNumber, user)) //сравнивает ввел ли пользователь с=корректный намб
                    {
                        CreateNewPhoneNumber(user);//изменениее
                    }
                    else
                    {
                        throw new Exception("Вы ввели некоректный номер");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void CreateNewPhoneNumber(BankUser user) //изменение после проверки
        {
            Console.Write("Введите новый номер телефона: ");
            try
            {

                if (string.IsNullOrEmpty(user.PhoneNumber = Console.ReadLine()))
                {
                    throw new Exception("Поле не может быть пустым");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        private bool CompareNumbers(string number, BankUser user) //сравнивание номеров
        {
            return number == user.PhoneNumber;
        }
        private void ShowHiddenNumber(string number) 
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

        //
        public override string ToString()
        {
            return $"ID:{ID}\n{base.ToString()}\nДата рождения: {BDate.Date.ToShortDateString()}\nНомер телефона: {PhoneNumber}";
        }

        //PASSWORD
        private void ChangePassword(BankUser user)
        {
            Console.WriteLine(@"Вы действительно желаете сменить пароль? : [д]\[н] ");
            string? answ;
            try
            {
                if (string.IsNullOrEmpty(answ = Console.ReadLine()))
                {

                    throw new Exception("Некорректный ответ");
                }
                else
                {
                    switch (answ)
                    {
                        case "д":
                            {
                                ConfirmPassword(user);
                                break;
                            }
                        case "н":
                            {
                                Console.WriteLine("Возвращаем...");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Некорректный ответ");
                            }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void ConfirmPassword(BankUser user) 
        {
            Console.Write("Введите текущий пароль: ");
            try
            {
                string? pass;
                if (string.IsNullOrEmpty(pass = Console.ReadLine()))
                {
                    throw new Exception("Пароль не может быть пустым");
                }
                else
                {
                    if (ComparePass(pass, user))
                    {
                        CreateNewPassword(user);
                    }
                    else
                    {
                        throw new Exception("Некорректный пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public bool ComparePass(string pass, BankUser user)
        {
            return pass == user.Password;
        }

        private void CreateNewPassword(BankUser user) //создание нового
        {
            Console.Write("Введите новый пароль: ");
            if (string.IsNullOrEmpty(user.Password = Console.ReadLine()))
            {
                throw new Exception("Пароль не может быть пустым");
            }
        }

        //ОТКРЫТИЕ КАРТЫ
        public void OpenNewCard() //TODO:обернуть при использовании в try\catch
        {
            
            if(CanUserOpenNewCard())
            {

                Console.Write("Доступные валюты: ");
                ShowAvailibleCurrency();
                Console.Write("Введите название валюты для новой карты: ");
                string currency;
                if (string.IsNullOrEmpty(currency = Console.ReadLine()))
                {
                    throw new Exception("Вы ввели пустую строку");
                }
                else
                {
                    if (Enum.TryParse(currency, true, out CurrencyType cur))
                    {
                        AddNewCard(new Card(CreateNewPinForCard(), cur));

                    }
                }
            }
        }


        private void ShowAvailibleCurrency()//показывает доступные валюты
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

        private bool HaveUserCurrency(CurrencyType currency)//проверка на существование открытой карты с такой же валютой
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

        private bool CanUserOpenNewCard() //может ли открыть карту новую
        {
            foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
            {
                if (!HaveUserCurrency(currency))
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

        private string CreateNewPinForCard()//создание ПИН
        {
            Console.Write("Введите новый PIN(4 символа): ");
            string newPin;
            if(string.IsNullOrEmpty(newPin = Console.ReadLine()) || newPin.Length < 4)
            {
                throw new Exception("Введен некорректный PIN\n");
            }
            else
            {
                return newPin;
            }
        }

        //FULL INFA PO KARTAM

        public void ShowAllCards() 
        {
            foreach(var el in UserCards)
            {
                Console.WriteLine(el);
            }
        }


        //ТРАНЗАКЦИИЫ
        public void MyTransaction()
        {

            MyTransactionMenu();
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                {
                     ShowMySendedTransaction();
                     break;
                }
                case "2":
                {
                    ShowCompletedTransaction();
                    break;
                }
            }

        }



        private void MyTransactionMenu() //TODO:перенести в ЮзерМеню
        {
            Console.WriteLine("1) Показать все отправленые транзакции");
            Console.WriteLine("2) Показать все принятые транзакции");
            Console.WriteLine("3) Показать общую сумму отправленных средств");
            Console.WriteLine("----");
            Console.WriteLine("Введите ответ: ");
        }

        ///ТРАНЗАКЦИИ ОТПРАВЛЕННЫЕ
        private void ShowMySendedTransaction()
        {
            foreach(Card card in UserCards)
            {
                ShowSendTransByCard(card.GetAllTransactions());
            }
        }


        private void ShowSendTransByCard(List<Transaction> cardTransaction) //конкретной карты
        {
            foreach(Transaction transaction in cardTransaction)
            {
                if(IsSendByMe(transaction))
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
        }

        private bool IsSendByMe(Transaction trans) //пользователем ли отправлена?
        {
            return trans.SenderInitials == Name;
        }
        
        //ТРАНЗАКЦИИ ПРИНЯТЫЕ


        private void ShowCompletedTransaction() //общая для получатель(Мы)
        {
            foreach(Card card in UserCards)
            {
                ShowCompletedTransactionByCard(card.GetAllTransactions());
            }
        }

        private void ShowCompletedTransactionByCard(List<Transaction> cardTransaction) //по каждой карте траназкция в которой мы получатель
        {
            foreach(Transaction transaction in cardTransaction)
            {
                if (IsCompleted(transaction))
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
        }
        
        private bool IsCompleted(Transaction transaction) //являемся ли мы получателем
        {
            return transaction.RecipientName == Name;
        }

        //СУММА ОТПРАВЛЕННЫХ

        //TODO:остановился тут 15:55
    }




    
}
