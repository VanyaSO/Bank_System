namespace Bank_System;

public static class PersonMenu
{
    

    public static void Menu()
    {

        BankUser? user = new BankUser();

        if(Common.User.UserRole == Role.BankUser)
        {
            user = Common.User as BankUser;
        }

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

                    user.OpenNewCard();
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
                Console.WriteLine("Заблокировать карту");
                //Todo: Заблокировать карту
                Console.Write("Введите номер карты котрую хотите заблокировать: ");
                string cardNumber = Console.ReadLine();

                try
                {
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
            case 0:
                return;
        }

        Menu();
    }

   
}