namespace FinanceControl.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
        public double Value { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
