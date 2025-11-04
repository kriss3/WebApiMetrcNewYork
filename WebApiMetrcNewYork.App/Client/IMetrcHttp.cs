using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Client;

//public interface IMetrcHttp
//{
//	Task<(int Status, string Body)> GetAsync(string absoluteUrl, CancellationToken ct = default);
//	Task<(int Status, string Body)> PostAsync(string absoluteUrl, string jsonBody, CancellationToken ct = default);
//}

/// <summary>
/// This is the alternative approach with ApiEnvelop. Mimics Result<TSuccess, TFailure>
/// </summary>
public interface IMetrcHttp
{
	Task<ApiEnvelope> GetAsync(string absoluteUrl, CancellationToken ct = default);
	Task<ApiEnvelope> PostAsync(string absoluteUrl, string jsonBody, CancellationToken ct = default);
}
