using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcWebhooksService : IMetrcWebhooksService
{
    public Task<ApiEnvelope> GetSubscriptionsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<ApiEnvelope> SubscribeToPackageWebhooksAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
