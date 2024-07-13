namespace Walutoszpieg.Model
{
    public class HistoricalExchangeRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
