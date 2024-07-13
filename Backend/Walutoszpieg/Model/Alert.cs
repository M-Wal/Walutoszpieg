namespace Walutoszpieg.Model
{
    public class Alert
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public string AlertType { get; set; }
        public decimal Threshold { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? NotifiedAt { get; set; }
    }
}
