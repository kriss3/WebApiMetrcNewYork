using Microsoft.Extensions.Options;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcWebHooksService(
    IHttpClientFactory httpClientFactory,
	IOptions<MetrcOptions> opts) : IMetrcWebHooksService
{
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
	private readonly MetrcOptions _opts = opts.Value;

	public Task<ApiEnvelope> GetSubscriptionsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
