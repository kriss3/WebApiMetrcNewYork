using System.Net;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcPackagesService
{
	Task<ApiEnvelope> GetActiveAsync(CommonQueryParams q, CancellationToken ct);
	Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct);
}