using System.Net;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcPackagesService
{
	Task<(HttpStatusCode status, string json)> GetActiveAsync(
		DateTimeOffset? lastModifiedStart, DateTimeOffset? lastModifiedEnd, CancellationToken ct);

	Task<(HttpStatusCode status, string json)> GetByLabelAsync(string label, CancellationToken ct);
}