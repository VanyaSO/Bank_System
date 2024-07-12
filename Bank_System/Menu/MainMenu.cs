namespace Bank_System;

public static class MainMenu
{
    public static int GetActionMenu(int max, int min = 0) {
        int action;
        while(true)
        {
            try
            {
                action = Common.GetAction(max, min);
                break;
            }
            catch (Exception error)
            {
                Message.ErrorMessage(error.Message);
            }
        }
        return action;
    }
    
    public static void Menu()
    {
        

        Console.WriteLine("Главное меню");
        Console.WriteLine("1) Вход");
        Console.WriteLine("2) Регистрация");
        Console.WriteLine("0) Завершить работу програмы");

        int action = GetActionMenu(2);
        switch (action)
        {
            case 1:
                try
                {

                    MainUser.LogIn();
                  
                    if (Common.User.UserRole == Role.BankUser)
                    {
                        Message.SuccessMessage("Вход в аккаунт выполнен успешно");
                        Message.SuccessMessage($"Добро пожаловать {Common.User.Name}");
                        BankUserMenu.Menu();

                    }
                    else if(Common.User.UserRole == Role.Admin)
                    {
                        Message.SuccessMessage("Вы вошли в аккаунт администратора");
                        AdminMenu.Menu();
                        
                    }

                }
                catch (AccessViolationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Хотите зарегать акк 1 - Yes | 2 - No"); //изменить
                    int action2 = GetActionMenu(2);
                    switch (action2)
                    {
                        case 1:
                            BankUser newUser1 = new BankUser(); //переделать
                            newUser1.Registration();
                            Common.User = newUser1;
                            break;
                        case 2:
                            break;
                    }
                }
                break;
            case 2:
                Console.WriteLine("Делаем Регистрацию");
                //Todo: регистарция в банк

                Common.User = new BankUser();
                (Common.User as BankUser).Registration();
                break;
            case 0:
                return;
        }


        //Проверка на роль
        
        
        Menu();
    }
}