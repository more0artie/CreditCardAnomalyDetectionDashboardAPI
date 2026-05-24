using TransactionSimulator.DTO;
using TransactionSimulator.Model;
namespace TransactionSimulator.Service
{
    public class FeatureBuilder
    {
		private readonly Random _rand = new();

		public TransactionFeatures Build(Transaction txn)
		{
			var features = new TransactionFeatures
			{
				Time = (float)(txn.Timestamp - DateTime.UtcNow.Date).TotalSeconds,
				Amount = (float)txn.Amount
			};

			SetSyntheticVFeatures(features, txn);

			return features;
		}

		private void SetSyntheticVFeatures(TransactionFeatures f, Transaction txn)
		{
			for (int i = 1; i <= 28; i++)
			{
				SetV(f, i, Gaussian(0, 1));
			}
		}
		private void SetV(TransactionFeatures f, int index, float value)
		{
			switch (index)
			{
				case 1: f.V1 = value; break;
				case 2: f.V2 = value; break;
				case 3: f.V3 = value; break;
				case 4: f.V4 = value; break;
				case 5: f.V5 = value; break;
				case 6: f.V6 = value; break;
				case 7: f.V7 = value; break;
				case 8: f.V8 = value; break;
				case 9: f.V9 = value; break;
				case 10: f.V10 = value; break;
				case 11: f.V11 = value; break;
				case 12: f.V12 = value; break;
				case 13: f.V13 = value; break;
				case 14: f.V14 = value; break;
				case 15: f.V15 = value; break;
				case 16: f.V16 = value; break;
				case 17: f.V17 = value; break;
				case 18: f.V18 = value; break;
				case 19: f.V19 = value; break;
				case 20: f.V20 = value; break;
				case 21: f.V21 = value; break;
				case 22: f.V22 = value; break;
				case 23: f.V23 = value; break;
				case 24: f.V24 = value; break;
				case 25: f.V25 = value; break;
				case 26: f.V26 = value; break;
				case 27: f.V27 = value; break;
				case 28: f.V28 = value; break;
				default: throw new ArgumentOutOfRangeException(nameof(index));
			}
		}
		private float Gaussian(double mean, double stdDev)
		{
			var u1 = 1.0 - _rand.NextDouble();
			var u2 = 1.0 - _rand.NextDouble();
			var randStdNormal =
				Math.Sqrt(-2.0 * Math.Log(u1)) *
				Math.Sin(2.0 * Math.PI * u2);

			return (float)(mean + stdDev * randStdNormal);
		}

	}
}
