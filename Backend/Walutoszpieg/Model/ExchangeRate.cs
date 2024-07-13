namespace Walutoszpieg.Model
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
