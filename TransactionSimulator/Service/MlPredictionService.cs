using TransactionSimulator.DTO;
using TransactionSimulator.Model;

namespace TransactionSimulator.Service
{
    public class MlPredictionService
    {
		private readonly HttpClient _httpClient;

		public MlPredictionService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<FraudPrediction>> PredictBatchAsync(
			List<TransactionFeatures> features)
		{
			var response = await _httpClient.PostAsJsonAsync(
				"/predict-batch",
				features
			);

			var text = await response.Content.ReadAsStringAsync();
			Console.WriteLine(text);   // 👈 prints the Python error

			response.EnsureSuccessStatusCode();

			return await response.Content
				.ReadFromJsonAsync<List<FraudPrediction>>();
		}

	}
}
