namespace Bank_System;

public static class FileSystem
{
    public static void SaveBank()
    {
        using (FileStream fs = new FileStream(Common.PathBankFileBin, FileMode.Create))
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
    public static void LoadBank()
    {
        using (FileStream fs = new FileStream(Common.PathBankFileBin, FileMode.Open))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                string name = br.ReadString();
                CurrencyType currency = br.ReadString().ParseToCurrencyType();
                double feeSending = br.ReadDouble();
                double feeReceipt = br.ReadDouble();

                Common.Bank = new Bank(name, currency, feeSending, feeReceipt);
            }
        }
    }

    public static void SaveUsers()
    {
        using (FileStream fs = new FileStream(Common.PathUsersFileBin, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                foreach (var user in Common.Bank.Users)
                {
                    bw.Write(user.Name);
                    bw.Write(user.Login);
                    bw.Write(user.Password);
                    bw.Write(user.UserRole.ToString());
                    
                    if (user.UserRole == Role.BankUser)
                    {
                        BankUser bUser = user as BankUser;
                        bw.Write(bUser.PhoneNumber);
                        bw.Write(bUser.BDate.ToString());
                        bw.Write(bUser.ID);
                        SaveUserCards(bw, bUser.UserCards);
                    }
                }
            }
        }
    }

    public static void LoadUsers()
    {
        using (FileStream fs = new FileStream(Common.PathUsersFileBin, FileMode.Open))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                while(fs.Length>br.BaseStream.Position)
                {
                    string name = br.ReadString();
                    string login = br.ReadString();
                    string password = br.ReadString();
                    string role = br.ReadString();

                
                    // догружаем данные если это обычный пользователь банка
                    if (role.ParseToRoleType() == Role.BankUser)
                    {
                        string phoneNumber = br.ReadString();                        
                        DateOnly bDate = DateOnly.Parse(br.ReadString());
                        string id = br.ReadString();
                        List<Card> cards = LoadUserCards(br);
                        
                        Common.Bank.Users.Add(new BankUser(name, login, password, phoneNumber, id, bDate, cards));
                    }
                    else
                    {
                        Common.Bank.Users.Add(new Admin(name, login, password));
                    }
                }
            }
        }
    }

    private static void SaveUserCards(BinaryWriter bw, List<Card> cards)
    {
        bw.Write(cards.Count);
        foreach (var card in cards)
        {
            bw.Write(card.CardNumber);
            bw.Write(card.PinCode);
            bw.Write(card.Currency.ToString());
            bw.Write(card.Balance);
            bw.Write(card.Status.ToString());
            // SaveCardTransactions(bw, card.GetAllTransactions());
        }
    }

    private static List<Card> LoadUserCards(BinaryReader br)
    {
        int countCards = br.ReadInt32();
        List<Card> cards = new List<Card>();
        
        for (int i = 0; i < countCards; i++)
        {
            string cardNumber = br.ReadString();
            string pinCode = br.ReadString();
            CurrencyType currency = br.ReadString().ParseToCurrencyType();
            decimal balance = br.ReadDecimal();
            CardStatus status = br.ReadString().ParseToCardStatus();
            
            cards.Add(new Card(cardNumber, pinCode, currency, balance, status));
        }
        return cards;
    }

    // public static void SaveCardTransactions(BinaryWriter bw, List<Transaction> transactions)
    // {
    //     bw.Write(transactions.Count);
    //     foreach (var trans in transactions)
    //     {
    //         bw.Write(trans.TransactionTime.ToString());
    //         bw.Write(trans.Amount);
    //         
    //     }
    // }
}