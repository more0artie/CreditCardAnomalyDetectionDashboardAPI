using Microsoft.AspNetCore.Mvc;
using TransactionSimulator.DTO;
using TransactionSimulator.Service;
using TransactionSimulator.Model;

namespace TransactionSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
		private readonly TransactionGenerator _transactionGenerator;
		private readonly FeatureBuilder _featureBuilder;
		private readonly MlPredictionService _mlPredictionService;

		public TransactionController(TransactionGenerator transactionGenerator, FeatureBuilder featureBuilder, MlPredictionService mlService)
		{
			_transactionGenerator = transactionGenerator;
			_featureBuilder = featureBuilder;
			_mlPredictionService = mlService;
		}

		[HttpGet("generate")]
		public IActionResult Generate()
		{
			var txn = _transactionGenerator.Generate();
			var features = _featureBuilder.Build(txn);

			return Ok(new
			{
				Transaction = txn,
				Features = features
			});
		}
		[HttpGet("generate-batch")]
		public IActionResult GenerateBatch([FromQuery] int count = 20)
		{
			var transactions = new List<Transaction>();
			var featuresList = new List<TransactionFeatures>();

			for (int i = 0; i < count; i++)
			{
				var txn = _transactionGenerator.Generate();
				var features = _featureBuilder.Build(txn);

				transactions.Add(txn);
				featuresList.Add(features);
			}

			return Ok(new
			{
				//Transactions = transactions,
				Features = featuresList
			});
		}
		[HttpGet("generate-and-predict")]
		public async Task<IActionResult> GenerateAndPredict(
	[FromQuery] int count = 20)
		{
			var transactions = new List<Transaction>();
			var featuresList = new List<TransactionFeatures>();

			for (int i = 0; i < count; i++)
			{
				var txn = _transactionGenerator.Generate();
				var features = _featureBuilder.Build(txn);

				transactions.Add(txn);
				featuresList.Add(features);
			}

			// 🔥 CALL PYTHON ML API
			var predictions = await _mlPredictionService.PredictBatchAsync(featuresList);

			// 🔗 Merge results
			var result = transactions.Select((txn, index) => new
			{
				Transaction = txn,
				Prediction = predictions[index]
			});

			return Ok(result);
		}



	}
}
