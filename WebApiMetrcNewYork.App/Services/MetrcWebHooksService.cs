using Microsoft.Extensions.Options;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcWebHooksService(
    IHttpClientFactory httpClientFactory,
	IOptions<MetrcOptions> opts) : IMetrcWebHooksService
{
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
	private readonly MetrcOptions _opts = opts.Value;

	public async Task<ApiEnvelope> GetSubscriptionsAsync(CancellationToken ct)
    {
		var client = _httpClientFactory.CreateClient("MetrcWebHooks");
		using var resp = await client.GetAsync("/webhooks/v2", ct);
		return await MetrcEnvelopeFactory.FromResponseAsync(resp, ct);
	}

    public Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
