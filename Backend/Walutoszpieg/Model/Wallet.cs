namespace Walutoszpieg.Model
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
    public class Rate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public decimal Mid { get; set; }
    }

    public class ExchangeRateTable
    {
        public string Table { get; set; }
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }
    public class ConvertCurrencyRequest
    {
        public int UserId { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
    }

   
    
}
