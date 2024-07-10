namespace Bank_System;

public class Card
{
    public string CardNumber { get; private set; }
    private string _pinCode;
    public CurrencyType Currency { get; private set; }
    public decimal Balance { get; private set; }
    public CardStatus Status {  get; set; }
    private List<Transaction> _transactions = new List<Transaction>();

    public Card(string pinCode, CurrencyType currency, decimal initialBalance = 0)
    {
        CardNumber = GenerateCardNumber();
        _pinCode = pinCode;
        Currency = currency;
        Status = CardStatus.Active;
        Balance = initialBalance;
        
    }

    public Card(CurrencyType currency, decimal initialBalance = 0)
    {
        CardNumber = GenerateCardNumber();
        
        Currency = currency;
        Status = CardStatus.Active;
        Balance = initialBalance;

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
        return _pinCode == pin;
    }

    private bool IsValidePin(string pin)
    {
        return pin.Length == 4 && int.TryParse(pin, out _);
    }

    public void SetPinCode(string pin)
    {
        if (IsValidePin(pin))
        {
            _pinCode = pin;
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
    
    public void Transfer(Card recipientCard, decimal amount, string senderInitials, double feesend, double feereceipt, decimal? exchangeRate = null, string recipientName = null)
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
            if (exchangeRate == null)
            {
                throw new ArgumentException("Exchange rate must be provided for transactions to different currencies.");
            }

            // TODO: пересмотреть еще раз после добавления QuerrySystem
            
            decimal exchangedAmount = amount / exchangeRate.Value;

            decimal calcFee = (decimal)feereceipt / 100;
            
            recipientCard.Deposit(exchangedAmount * calcFee);
        }
        else
        {
            decimal calcFee = (decimal)feereceipt / 100;
            recipientCard.Deposit(amount * calcFee);
        }

        decimal calcThisFee = (decimal)feesend / 100;
        Withdraw(amount * calcThisFee);

        // добавил транзакции для обеих сторон
        AddTransaction(new Transaction(
            senderCard: this,
            amount: amount,
            recipientCard: recipientCard,
            senderInitials: senderInitials,
            exchangeRate: exchangeRate,
            recipientName: recipientName
        ));

        recipientCard.AddTransaction(new Transaction(
            senderCard: recipientCard,
            amount: amount,
            recipientCard: this,
            senderInitials: senderInitials,
            exchangeRate: exchangeRate,
            recipientName: recipientName
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

