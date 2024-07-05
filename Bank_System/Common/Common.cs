namespace Bank_System;

public static class Common
{
    public static Bank Bank;

    public static int GetAction(int maxVarAction, int minVarAction)
    {
        int action;

        if (!Int32.TryParse(Console.ReadLine(), out action))
            throw new Exception("Введённое значение не является числом");

        if (action < minVarAction || action > maxVarAction)
            throw new Exception("Введённое число выходит за пределы допустимого диапазона");

        return action;
    }

    public static void StartProgram()
    {
        Query.GetСurrencyRates();
    }
}