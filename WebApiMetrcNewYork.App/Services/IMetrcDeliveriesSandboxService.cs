using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

/// <summary>
/// Metrc sandbox delivery operations (GET active deliveries, POST new delivery).
/// Thin proxy layer over Metrc sandbox API, returning ApiEnvelope for consistency.
/// </summary>
public interface IMetrcDeliveriesSandboxService
{
	Task<ApiEnvelope> GetActiveAsync(CommonQueryParams q, CancellationToken ct);
	Task<ApiEnvelope> CreateAsync(object payload, CancellationToken ct);
}
