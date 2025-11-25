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

    public async Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct)
    {
		var client = _httpClientFactory.CreateClient("MetrcWebHooks");

		// Now, this is important. This must match the inbound controller route
		// This is a callback url Metrc will send updates on packages.
		var receiverUrl =
			$"{_opts.WebhookReceiverBaseUrl.TrimEnd('/')}/api/metrc-webhooks/packages/inbound";

		var body = new[]
		{
			new
			{
				objectType = "Package",
				url = receiverUrl,
				verb = "POST",
				status = "Active",

                // From your existing secrets.json:
                userApiKey = _opts.UserApiKey,
				tpiApiKey = _opts.VendorApiKey,          // integrator key

                serverPublicKeyFingerprint = (string?)null,
				template = "{\"data\":#DATA#, \"datacount\":#DATACOUNT#}",
				errorResponseJsonTemplate = "{\"error\": #ERRORMESSAGE#, \"errorcode\": #ERRORCODE#}",

                // licenseNumber doubles as facility license number here
                facilityLicenseNumbers = new[] { _opts.LicenseNumber }
			}
		};

		using var resp = await client.PutAsJsonAsync("/webhooks/v2", body, ct);
		return await MetrcEnvelopeFactory.FromResponseAsync(resp, ct);


		throw new NotImplementedException();
    }
}
