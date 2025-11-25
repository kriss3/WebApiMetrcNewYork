using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcWebHooksService
{
	Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct);
	Task<ApiEnvelope> GetSubscriptionsAsync(CancellationToken ct);
}
