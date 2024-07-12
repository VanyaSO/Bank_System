namespace Bank_System;

public class Card
{
    public string CardNumber { get; }
    public string PinCode { get; private set; }
    public CurrencyType Currency { get; }
    public decimal Balance { get; private set; }
    public CardStatus Status {  get; set; }
    public List<Transaction> _transactions = new List<Transaction>();

    public Card(string pinCode, CurrencyType currency, decimal initialBalance = 0)
    {
        CardNumber = GenerateCardNumber();
        PinCode = pinCode;
        Currency = currency;
        Status = CardStatus.Active;
        Balance = initialBalance;
    }

    public Card(string cardNumber, string pinCode, CurrencyType currency, decimal balance, CardStatus status)
    {
        CardNumber = cardNumber;
        PinCode = pinCode;
        Currency = currency;
        Balance = balance;
        Status = status;
    }


    private static string GenerateCardNumber()
    {
        const int cardNumberLength = 16;
        char[] cardNumber = new char[cardNumberLength];

        for (int i = 0; i < cardNumberLength; i++)
        {
            cardNumber[i] = (char)('0' + Common.Random.Next(0, 10));
        }

        return new string(cardNumber);
    }

    public bool VerifyPinCode(string pin)
    {
        return PinCode == pin;
    }

    private bool IsValidePin(string pin)
    {
        return pin.Length == 4 && int.TryParse(pin, out _);
    }

    public void SetPinCode(string pin)
    {
        if (IsValidePin(pin))
        {
            PinCode = pin;
        }
        else
        {
            throw new ArgumentException("Invalid pin code. It must be 4 digits.");
        }
    }
    
    public void Deposit(decimal amount) // НЕ ТРАНЗАКЦИЯ!! это просто закинуть денбги через терминал условный
    {
        if (Status == CardStatus.Blocked)
        {
            throw new InvalidOperationException("Cannot perform this operation because the card is blocked.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be greater than zero.");
        }

        Balance += amount;
    }
    
    public void Withdraw(decimal amount) // ТОЖЕ НЕ ТРАНЗАКЦИЯ!! это снять деньги в банкомате
    {
        if (Status == CardStatus.Blocked)
        {
            throw new InvalidOperationException("Cannot perform this operation because the card is blocked.");
        }
        
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be greater than zero.");
        }
        
        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
    }
    
    public void BlockCard()
    {
        Status = CardStatus.Blocked;
    }
    
    public void UnblockCard()
    {
        Status = CardStatus.Active;
    }

    public override string ToString()
    {
        return $"Номер карты: {CardNumber} \nВалюта: {Currency} \nБаланс: {Balance} \nСтатус: {Status} \n";
    }
    

    public void Transfer(Card recipientCard, decimal amount, string senderInitials)
    {
        if (recipientCard == null)
        {
            throw new ArgumentNullException(nameof(recipientCard));
        }
        
        if (amount <= 0)
        {
            throw new ArgumentException("Transfer amount must be greater than zero.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        if (this.Currency != recipientCard.Currency)
        {
            if (Common.Bank.Currencies == null)
            {
                throw new ArgumentException("Exchange rate must be provided for transactions to different currencies.");
            }

            // TODO: пересмотреть еще раз после добавления QuerrySystem
            
            decimal exchangedAmount = amount * (decimal)Common.Bank.Currencies[this.Currency] / (decimal)Common.Bank.Currencies[recipientCard.Currency];

            decimal calcFee = (decimal)Common.Bank.FeeReceipt / 100;

            if (calcFee != 0)
                recipientCard.Deposit(exchangedAmount - exchangedAmount * calcFee);
            else
                recipientCard.Deposit(exchangedAmount);
        }
        else
        {
            decimal calcFee = (decimal)Common.Bank.FeeReceipt / 100;
            recipientCard.Deposit(amount * calcFee);
        }

        decimal calcThisFee = (decimal)Common.Bank.FeeSending / 100;
        if (calcThisFee != 0)
            Withdraw(amount + amount * calcThisFee);
        else
            Withdraw(amount);
        

        // добавил транзакции для обеих сторон
        AddTransaction(new Transaction(
            senderCard: this,
            amount: amount,
            recipientCard: recipientCard,
            senderInitials: senderInitials,
            exchangeRate: (decimal)Common.Bank.Currencies[this.Currency] / (decimal)Common.Bank.Currencies[recipientCard.Currency]
        ));

        recipientCard.AddTransaction(new Transaction(
            senderCard: recipientCard,
            amount: amount,
            recipientCard: this,
            senderInitials: senderInitials,
            exchangeRate: (decimal)Common.Bank.Currencies[this.Currency] / (decimal)Common.Bank.Currencies[recipientCard.Currency]
        ));
    }

    private void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public List<Transaction> GetAllTransactions()
    {
        return _transactions;
    }
}

