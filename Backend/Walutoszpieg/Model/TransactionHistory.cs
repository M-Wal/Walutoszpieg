namespace Walutoszpieg.Model
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyFromId { get; set; }
        public string CurrencyToId { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
