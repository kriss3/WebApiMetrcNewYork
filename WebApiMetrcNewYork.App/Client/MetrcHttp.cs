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

}
