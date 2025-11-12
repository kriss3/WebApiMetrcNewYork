using System.Net;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcPackagesService
{
	Task<ApiEnvelope> GetActiveAsync(
		DateTimeOffset? lastModifiedStart,
		DateTimeOffset? lastModifiedEnd,
		CancellationToken ct);

	Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct);
}