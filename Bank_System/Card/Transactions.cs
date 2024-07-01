namespace Bank_System;

public class Transaction
{
    // Время транзакции 
    // Сумма
    // Карта получателя
    // Имя Фамилия ? - если перевод на карту своего банка то есть - если перевод на карту другого банка нету🙂 
    // Валюта - если была отправка на другую валютную карту показываем курс
    public DateTime TransactionTime { get; private set; }
    public decimal Amount { get; private set; }
    public Card RecipientCard { get; private set; }
    public Card SenderCard { get; private set; }
    public string RecipientName { get; private set; } // не наши инициалы (получателя)
    public decimal? ExchangeRate { get; private set; } // курс обмена валют (если перевод на краты разных валют)
    public string SenderInitials { get; private set; } // наши инициалы

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
        
        // Если валюта одна и та же, курс обмена не нужен
        if (exchangeRate != null)
        {
            throw new ArgumentException("Exchange rate should not be provided for transactions within the same currency.");
        }
    }

    public void DisplayTransactionDetails()
    {
        Console.WriteLine($"Время транзакции: {TransactionTime}");
        Console.WriteLine($"Сумма транзакции: {Amount:C}");
        Console.WriteLine($"Номер катры отправителя: {SenderCard.CardNumber}");
        Console.WriteLine($"Инициалы отправителя: {SenderInitials}");
        Console.WriteLine($"Номер карты получателя: {RecipientCard.CardNumber}");
        if (RecipientName != null)
        {
            Console.WriteLine($"Имя получателя: {RecipientName}");
        }

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