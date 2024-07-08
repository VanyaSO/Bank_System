using System.Globalization;

namespace Bank_System;

public static class BankUserMenu
{
    

    public static void Menu()
    {

        BankUser? user = Common.User as BankUser;


        Console.WriteLine("Меню пользователя");
        Console.WriteLine("1) Отправить");
        Console.WriteLine("2) Открыть карту");
        Console.WriteLine("3) Мои карты");
        Console.WriteLine("4) Заблокировать карту");
        Console.WriteLine("5) Курс валют");
        Console.WriteLine("6) Мои транзакции");
        Console.WriteLine("0) Выйти");

        int action = MainMenu.GetActionMenu(6);
        switch (action)
        {
            case 1:
                Console.WriteLine("Отправить");
                //Todo: отправка валюты
                break;
            case 2:
                Console.WriteLine("Открыть карту");
                try
                {
                    if (user.CanUserOpenNewCard()) //проверка есть ли доступные валюты
                    {

                        Console.WriteLine("Список доступных валют: ");
                        user.ShowAvailibleCurrency();

                        Console.WriteLine("Введите валюту для открытия: ");
                        string inputCurrency;
                        if (string.IsNullOrEmpty(inputCurrency = Console.ReadLine()))
                        {
                            throw new Exception("Вы ввели пустую строку");
                        }
                        else
                        {
                            CurrencyType currency;
                            if (!Enum.TryParse(inputCurrency, out currency))
                            {
                                throw new Exception("Некорректное название валюты");
                            }
                            else if (user.HaveUserCurrency(currency)) //проверяем не ввел ли юзер та которая уже есть
                            {
                                throw new Exception("Карта с такой валютой уже открыта");
                            }
                            else
                            {

                                Console.Write("Введите PIN(4 символа): ");
                                string newPin;
                                if (string.IsNullOrEmpty(newPin = Console.ReadLine()) || newPin.Length == 4)
                                {
                                    throw new Exception("Некорректный PIN");
                                }
                                else
                                {
                                    user.OpenNewCard(newPin, currency);

                                }
                            }
                        }


                    }
                    else
                    {
                        throw new Exception("Вы открыли все доступные счета");
                    }   
                    
                }
                catch (Exception ex) {

                    Message.ErrorMessage(ex.Message);
                }   
                break;
            case 3:
                Console.WriteLine("Информация по моим картам");
                //Todo: Информация о картах
                user.ShowAllCards();
                break;
            case 4:
                //Todo: Заблокировать карту
                Console.WriteLine("Заблокировать карту");
                Console.WriteLine("Список активных карт: "); //показываем те которые не залоченые
                user.ShowAllActiveCards();
                

                try
                {
                    Console.Write("Введите номер карты котрую хотите заблокировать: ");
                    string cardNumber = Console.ReadLine();
                    user.BlockCard(cardNumber);

                }
                catch(Exception ex)
                {
                    Message.ErrorMessage(ex.Message);
                }
                break;
            case 5:
                Console.WriteLine("Курс валют");
                //Todo: Курс валют
                break;
            case 6:
                Console.WriteLine("Мои транзакции");
                //Todo: мои Транзакции


                ///Выдать список карт,запрос карты у юзера, вывод тразакции по карте(общий);
                break;

            case 7:
                Console.WriteLine("Изменить пароль");
                try
                {

                    string? compPass;
                    Console.Write("Введите текущий пароль: ");
                    if(string.IsNullOrEmpty(compPass = Console.ReadLine()))
                    {
                        throw new Exception("Вы ввели пустую строку");
                    }
                    else
                    {
                        if (user.ComparePass(compPass))
                        {
                            Console.Write("Ввелите новый пароль: ");
                            string? newPass;
                            if(string.IsNullOrEmpty(newPass = Console.ReadLine()))
                            {
                                throw new Exception("Вы ввели пустую строку");
                            }
                            else
                            {
                                user.ChangePassword(newPass);
                                Message.SuccessMessage("Пароль успешно изменен");
                            }
                        }
                        else
                        {
                            throw new Exception("Неправильный пароль!");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Message.ErrorMessage(ex.Message);
                }

                break;
            case 8:
                Console.WriteLine("Изменить телефон");
                try
                {
                    string? compNumber;
                    Console.Write("Текущий номер телефона: ");
                    user.ShowHiddenNumber(user.PhoneNumber);

                    Console.Write("Введите текущий номер телефона: ");
                    if(string.IsNullOrEmpty(compNumber = Console.ReadLine()))
                    {
                        throw new Exception("Вы ввели пустую строку");
                    }
                    else
                    {
                        if (user.CompareNumbers(compNumber))
                        {
                            string? newNumber;
                            Console.Write("Введите новый номер телефона: ");
                            if(string.IsNullOrEmpty(newNumber = Console.ReadLine()))
                            {
                                throw new Exception("Вы ввели пустую строку");
                            }
                            else
                            {

                                user.ChangePassword(newNumber);
                                Message.SuccessMessage("Номер телефона успешно изменен");
                            }
                        }
                        else
                        {
                            throw new Exception("Вы ввели неправильный номер");
                        }
                    }

                }
                catch(Exception ex)
                {
                    Message.ErrorMessage(ex.Message);
                }

                break;
            case 0:
                return;
        }

        Menu();
    }

   
}