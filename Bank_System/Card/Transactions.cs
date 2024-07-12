namespace Bank_System;

public class Transaction
{
    public DateTime TransactionTime { get; }
    public decimal Amount { get; }
    public Card RecipientCard { get; }
    public Card SenderCard { get; }
    public string RecipientName { get; }
    public decimal? ExchangeRate { get; } // курс обмена валют (если перевод на краты разных валют)
    public string SenderInitials { get; } // наши инициалы

    public Transaction(Card senderCard, decimal amount, Card recipientCard, string senderInitials, decimal? exchangeRate = null, string recipientName = null)
    {
        if (string.IsNullOrWhiteSpace(senderInitials)) throw new ArgumentException("Sender initials are required.", nameof(senderInitials));

        TransactionTime = DateTime.Now;
        Amount = amount;
        SenderCard = senderCard;
        RecipientCard = recipientCard;
        ExchangeRate = exchangeRate;
        RecipientName = recipientName;
        SenderInitials = senderInitials;
    }

    public void DisplayTransactionDetails()
    {
        Console.WriteLine($"Время транзакции: {TransactionTime}");
        Console.WriteLine($"Сумма транзакции: {Amount:C}");
        Console.WriteLine($"Номер катры отправителя: {SenderCard.CardNumber}");
        Console.WriteLine($"Инициалы отправителя: {SenderInitials}");
        Console.WriteLine($"Номер карты получателя: {RecipientCard.CardNumber}");
        Console.WriteLine($"Имя получателя: {RecipientName}");

        if (SenderCard.Currency == RecipientCard.Currency)
        {
            Console.WriteLine($"Перевод в валюте: {SenderCard.Currency}");
        }
        else
        {
            Console.WriteLine($"Валюта отправителя: {SenderCard.Currency}");
            Console.WriteLine($"Валюта получателя: {RecipientCard.Currency}");
        }
        if (ExchangeRate.HasValue)
        {
            Console.WriteLine($"Exchange Rate: {ExchangeRate.Value}");
        }
    }
}