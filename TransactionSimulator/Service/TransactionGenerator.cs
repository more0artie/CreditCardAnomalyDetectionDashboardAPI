//using System.Transactions;
using TransactionSimulator.Model;

namespace TransactionSimulator.Service
{
    public class TransactionGenerator
    {
		private readonly Random _rand = new();

		private static readonly string[] Merchants =
		{
			"Amazon",
			"Flipkart",
			"Zomato",
			"Swiggy",
			"Uber",
			"Netflix",
			"Myntra",
			"BigBasket",
			"IRCTC",
			"BookMyShow"
		};

		private static readonly string[] Locations =
		{
			"Bangalore",
			"Mumbai",
			"Delhi",
			"Hyderabad",
			"Chennai",
			"Pune",
			"Kolkata"
		};

		public Transaction Generate()
		{
			return new Transaction
			{
				TransactionId = Guid.NewGuid(),
				CardNumber = MaskedCard(),
				Amount = GenerateAmount(),
				Merchant = RandomFrom(Merchants),
				Location = RandomFrom(Locations),
				Timestamp = DateTime.UtcNow,
				Status = "Success"
			};
		}

		// -------- helpers --------

		private string MaskedCard()
		{
			return $"XXXX-XXXX-XXXX-{_rand.Next(1000, 9999)}";
		}

		private decimal GenerateAmount()
		{
			// Skew towards smaller transactions
			var amount = Math.Pow(_rand.NextDouble(), 2) * 10000;
			return Math.Round((decimal)amount, 2);
		}

		private string RandomFrom(string[] values)
		{
			return values[_rand.Next(values.Length)];
		}
}
}
