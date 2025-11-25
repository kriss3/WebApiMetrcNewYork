using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcWebhooksService
{
	Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct);
	Task<ApiEnvelope> GetSubscriptionsAsync(CancellationToken ct);
}
