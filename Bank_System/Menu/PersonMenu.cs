namespace Bank_System;

public static class PersonMenu
{
    public static void Menu()
    {
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
                //Todo: открыть карту
                break;
            case 3:
                Console.WriteLine("Информация по моим картам");
                //Todo: Информация о картах
                break;
            case 4:
                Console.WriteLine("Заблокировать карту");
                //Todo: Заблокировать карту
                break;
            case 5:
                Console.WriteLine("Курс валют");
                //Todo: Курс валют
                break;
            case 6:
                Console.WriteLine("Мои транзакции");
                //Todo: мои Транзакции
                break;
            case 0:
                return;
        }

        Menu();
    }
}