using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

/// <summary>
/// Metrc sandbox delivery operations (GET active deliveries, POST new delivery).
/// Thin proxy layer over Metrc sandbox API, returning ApiEnvelope for consistency.
/// </summary>
public interface IMetrcPackagesService
{
	Task<ApiEnvelope> GetActiveAsync(PackagesActiveQuery q, CancellationToken ct);
	Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct);
}
