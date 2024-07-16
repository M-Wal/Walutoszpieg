namespace Walutoszpieg.Model
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}
