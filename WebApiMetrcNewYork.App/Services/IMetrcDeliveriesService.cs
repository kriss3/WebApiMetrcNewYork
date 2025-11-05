using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public interface IMetrcDeliveriesService
{
	Task<ApiEnvelope> GetActiveAsync(string licenseNumber,
		string? pageNumber = null,
		string? pageSize = null,
		string? salesDateStart = null,
		string? salesDateEnd = null,
		string? lastModifiedStart = null,
		string? lastModifiedEnd = null,
		CancellationToken ct = default);

	Task<ApiEnvelope> GetByIdAsync(string id,
		string? licenseNumber = null, CancellationToken ct = default);

	Task<ApiEnvelope> PostDeliveriesAsync(string licenseNumber,
		string rawJsonArrayBody, CancellationToken ct = default);
}
// continue here tomorrow - update Service after interface was updated.

//public interface IMetrcDeliveriesService
//{
//	Task<(int Status, string Body)> GetActiveAsync(string licenseNumber, string? pageNumber = null, string? pageSize = null, string? salesDateStart = null, string? salesDateEnd = null, string? lastModifiedStart = null, string? lastModifiedEnd = null, CancellationToken ct = default);
//	Task<(int Status, string Body)> GetByIdAsync(string id, string? licenseNumber = null, CancellationToken ct = default);
//	Task<(int Status, string Body)> PostDeliveriesAsync(string licenseNumber, string rawJsonArrayBody, CancellationToken ct = default);
//}
