using System.Text;

namespace WebApiMetrcNewYork.App.Client;

public sealed class MetrcHttp(IHttpClientFactory factory) : IMetrcHttp
{
	private readonly IHttpClientFactory _factory = factory;

	public async Task<(int Status, string Body)> GetAsync(string absoluteUrl, CancellationToken ct = default)
	{
		var client = _factory.CreateClient("MetrcNY");
		var resp = await client.GetAsync(absoluteUrl, ct);
		var body = await resp.Content.ReadAsStringAsync(ct);
		return ((int)resp.StatusCode, body);
	}

	public async Task<(int Status, string Body)> PostAsync(string absoluteUrl, string jsonBody, CancellationToken ct = default)
	{
		var client = _factory.CreateClient("MetrcNY");
		using var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
		var resp = await client.PostAsync(absoluteUrl, content, ct);
		var body = await resp.Content.ReadAsStringAsync(ct);
		return ((int)resp.StatusCode, body);
	}
}
