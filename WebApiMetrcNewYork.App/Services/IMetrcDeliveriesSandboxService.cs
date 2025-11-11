using System.Net;

namespace WebApiMetrcNewYork.App.Services;

/// <summary>
/// Handles Metrc sandbox delivery operations (GET active deliveries, POST new delivery).
/// Thin proxy layer over Metrc sandbox API.
/// </summary>
public interface IMetrcDeliveriesSandboxService
{
	Task<(HttpStatusCode status, string json)> GetActiveAsync(CancellationToken ct);
	Task<(HttpStatusCode status, string json)> CreateAsync(object payload, CancellationToken ct);
}
