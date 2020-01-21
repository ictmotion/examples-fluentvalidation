namespace FluentValidationExample
{
    public class MoneyTransaction
    {
        public string Amount { get; set; }
        public string BankAccountFrom { get; set; }
        public string BankAccountTo { get; set; }
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public string TimeStamp { get; set; }
    }
}