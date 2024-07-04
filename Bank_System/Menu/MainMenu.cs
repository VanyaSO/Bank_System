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
        MainUser? user;

        Console.WriteLine("Главное меню");
        Console.WriteLine("1) Вход");
        Console.WriteLine("2) Регистрация");
        Console.WriteLine("0) Завершить работу програмы");

        int action = GetActionMenu(2);
        switch (action)
        {
            case 1:
                Console.WriteLine("Делаем Вход");
                
                //user = MainUser.LogIn();//TODO: добавит list всех юзеров

                break;
            case 2:
                Console.WriteLine("Делаем Регистрацию");
                //Todo: регистарция в банк


                BankUser newUser = new BankUser(); //переделать
                newUser.Registration();
                user = newUser;

                if(user!= null)
                {
                    Message.SuccessMessage("Вход выполнен успешно");
                }

                //Console.WriteLine(user);
                
                break;
            case 0:
                return;
        }

        Menu();
    }
}