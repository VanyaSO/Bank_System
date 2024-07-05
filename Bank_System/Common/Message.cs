namespace Bank_System;

public static class Message
{
    public static void ErrorMessage(string mess)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mess);
        Console.ResetColor();
    }
    
    public static void WarningMessage(string mess)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(mess);
        Console.ResetColor();
    }
    
    public static void SuccessMessage(string mess)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(mess);
        Console.ResetColor();
    }
}