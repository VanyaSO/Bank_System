using System.Security.Cryptography;

namespace Bank_System;

public static class AdminMenu
{
    public static void Menu()
    {
        Admin? admin = Common.User as Admin;

        Console.WriteLine("Меню администратора");
        Console.WriteLine("1) Изменить логин");
        Console.WriteLine("2) Изменить пароль");
        Console.WriteLine("3) Пользователи"); //Todo: Menu
        Console.WriteLine("4) Статистика"); //
        Console.WriteLine("0) Выйти");

        int action = MainMenu.GetActionMenu(4);
        
        switch (action)
        {
            case 1:
                Console.WriteLine("Изменить логин");
                //Todo: Изменить логин админ
                Console.WriteLine($"Текущий логин: {admin.Login}");
                try
                {

                    Console.Write("Введите новый логин: ");
                    string? newLogin;
                    if(string.IsNullOrEmpty(newLogin = Console.ReadLine()))
                    {
                        throw new Exception("Вы ввели пустую строку");
                    }
                    else
                    {
                        admin.ChangeLogin(newLogin);
                        Message.SuccessMessage("Логин успешно изменен");
                    }
                }
                catch (Exception ex)
                {
                    Message.ErrorMessage(ex.Message);
                }
                break;
            case 2:
                Console.WriteLine("Изменить пароль");
                try
                {
                    Console.Write("Введите текущий пароль: ");
                    string compPass;
                    if (string.IsNullOrEmpty(compPass = Console.ReadLine()))
                    {
                        throw new Exception("Вы ввели пустую строку");
                    }
                    else
                    {
                        if (admin.ComparePass(compPass))
                        {
                            Console.Write("Введите новый пароль: ");
                            string newPass;
                            if(string.IsNullOrEmpty (newPass = Console.ReadLine()))
                            {
                                throw new Exception("Вы ввели пустую строку");
                            }
                            else
                            {
                                admin.ChangePass(newPass);
                                Message.SuccessMessage("Пароль успешно изменен");
                            }
                        }
                        else
                        {
                            throw new Exception("Вы ввели неправильный пароль");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Message.ErrorMessage(ex.Message);
                }
                
                break;
            case 3:
                MenuUsers();
                break;
            case 4:
                Console.WriteLine("Статистика");
                MenuStatistic();
                break;
            case 0:
                return;
        }

        Menu();
    }

    public static void MenuUsers()
    {
        ShowAllUsers();
        
        // -----Вынестив в отельный метод для поиска пользователя ----- //
        Console.WriteLine("Введите ФИО или ID пользователя, для получаения большей информации о нём. Enter - вернутся назад");
        string findUserData = Console.ReadLine();
        if (findUserData.Trim().Length == 0)
            return;

        BankUser user = GetUserByData(findUserData);
        try
        {

            if(user != null)
            {
                // Todo: ищем пользователя
                // выводим инфу о найденом пользователе если нет запрашиваем еще раз 
                // ----- //
                Console.WriteLine("Выбраный пользователь: ");
                user.ShowUserInfo();



                // если есть заблокированные карты выводим вариант выбор вариант 1 и 0 если нет то только 0
                if (user.IsAnyBlocked())
                {
                    Console.WriteLine("1) Разблокировать карту");
                }
                Console.WriteLine("0) Вернуться назад");
                int action = MainMenu.GetActionMenu(1);
                switch (action)
                {
                    case 1:
                        Console.WriteLine("1) Разблокировать карту");
                        if (!user.IsAnyBlocked())
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Заблокрованные карты пользователя: ");

                            user.ShowAllBlockedCard();
                            Console.Write("Введите номер карты: ");
                            string searchedNumber = Console.ReadLine() ;
                            user.UnlockCard(searchedNumber);
                        }

                        break;
                    //case 2:
                    //    Console.WriteLine("0) Вернуться назад");
                        
                    //    break;
                    case 0:
                        return;
                }


            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }
        catch(Exception ex)
        {
            Message.ErrorMessage(ex.Message);
        }
        
        
         MenuUsers();
    }
    
    public static void MenuStatistic()
    {
        // убрать это 
        ShowAllUsers();

        Console.WriteLine("Введите ФИО или ID пользователя, для получаения большей информации о нём. Enter - вернутся назад");
        string findUserData = Console.ReadLine();
        if (findUserData.Trim().Length == 0)
            return;

        BankUser user = GetUserByData(findUserData);
        try
            {
            if(user!= null)
            {
                Console.WriteLine("Статистика");
                Console.WriteLine("1) Заработок на комиссиях по конкретному пользователю");
                Console.WriteLine("2) Заработок на комиссиях со всех пользователей");
                Console.WriteLine("0) Выйти");

                int action = MainMenu.GetActionMenu(2);
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Заработок на комиссиях по конкретному пользователю");
                        //Todo: Заработок на комиссиях по конкретному пользователю
                        // убрать вот сюда
                        double resultSum = user.GetSumOfComisionByUser();
                        Console.WriteLine($"{user.Name}: {resultSum}");
                        // предлагаем сохранить в текстовый файл
                        break;
                    case 2:
                        Console.WriteLine("Заработок на комиссиях по всем пользователям");


                        foreach(BankUser users in Common.Bank.Users)
                        {
                            user.GetSumOfComisionByUser();
                        }
                        //Todo: Заработок на комиссиях со всех пользователей - потом предлаем сохрнаить в текстовый файл
                        break;
                    case 0:
                        return;
                }
            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }
        catch (Exception ex) { 
            Message.ErrorMessage (ex.Message);
        }

        MenuStatistic();
    }

    //совет куда вынести методы
    public static void ShowAllUsers() 
    {
        foreach(MainUser user in Common.Bank.Users)
        {
            if (user.UserRole == Role.BankUser)
                (user as BankUser).ShowUserInfo();
        }
    }

    public static BankUser GetUserByData(string findData)
    {
        foreach(MainUser user in Common.Bank.Users)
        {
            if (user.UserRole == Role.BankUser)
                if(findData == user.Name || findData == (user as BankUser).ID)
                    return (user as BankUser);
        }

        return null;
    }

    public static void ShowAllCommisionUsers()
    {
        double fullSum = 0;
        foreach(BankUser user in Common.Bank.Users)
        {
            
            double sum = user.GetSumOfComisionByUser();
            fullSum += sum;
            Console.WriteLine($"{user.Name}: {sum}");
        }

        Console.WriteLine($"Общая сумма заработка на комиссии: {fullSum}");

    }
}