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
		var client = _httpClientFactory.CreateClient("MetrcWebHooks");

		// Now, this is important. This must match the inbound controller route as this is the 
		// fnct Metrc will call with updates.
		var receiverUrl =
			$"{_opts.WebhookReceiverBaseUrl.TrimEnd('/')}/api/metrc-webhooks/packages/inbound";


		throw new NotImplementedException();
    }

    public Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
