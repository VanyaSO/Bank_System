namespace Bank_System;

public static class FileSystem
{
    public static void SaveBankData()
    {
        string path = Common.CreatePath(Common.PathBankData);
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                Bank bank = Common.Bank;
                bw.Write(bank.Name);
                bw.Write(bank.Currency.ToString());
                bw.Write(bank.FeeSending);
                bw.Write(bank.FeeReceipt);
            }
        }
    }
}