namespace WebApiMetrcNewYork.App.Client;

public interface IMetrcHttp
{
	Task<(int Status, string Body)> GetAsync(string absoluteUrl, CancellationToken ct = default);
	Task<(int Status, string Body)> PostAsync(string absoluteUrl, string jsonBody, CancellationToken ct = default);
}
