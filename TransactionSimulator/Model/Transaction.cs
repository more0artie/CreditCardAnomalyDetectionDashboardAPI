namespace TransactionSimulator.Model
{
    public class Transaction
    {
		public Transaction() { }
		public Guid TransactionId { get; set; }
		public string CardNumber { get; set; }
		public decimal Amount { get; set; }
		public string Merchant { get; set; }
		public string Location { get; set; }
		public DateTime Timestamp { get; set; }
		public string Status { get; set; }
	}
}
