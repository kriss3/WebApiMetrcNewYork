using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcDeliveriesService(IMetrcHttp http) : IMetrcDeliveriesService
{
	private readonly IMetrcHttp _http = http;

	public Task<ApiEnvelope> GetActiveAsync(
		string licenseNumber, string?
		pageNumber = null,
		string? pageSize = null,
		string? salesDateStart = null,
		string? salesDateEnd = null,
		string? lastModifiedStart = null,
		string? lastModifiedEnd = null,
		CancellationToken ct = default)
		=> _http.GetAsync(MetrcUrls.GetActive(
			licenseNumber,
			pageNumber,
			pageSize,
			salesDateStart,
			salesDateEnd,
			lastModifiedStart,
			lastModifiedEnd), ct);

	public Task<ApiEnvelope> GetByIdAsync(
		string id, string? licenseNumber = null, CancellationToken ct = default)
		=> _http.GetAsync(MetrcUrls.GetById(id, licenseNumber), ct);

	//public Task<(int Status, string Body)> GetActiveAsync(
	//	string licenseNumber,
	//	string? pageNumber = null,
	//	string? pageSize = null,
	//	string? salesDateStart = null,
	//	string? salesDateEnd = null,
	//	string? lastModifiedStart = null,
	//	string? lastModifiedEnd = null,
	//	CancellationToken ct = default) => 
	//	_http.GetAsync(
	//		MetrcUrls.GetActive(licenseNumber, pageNumber, pageSize, salesDateStart, salesDateEnd, lastModifiedStart, lastModifiedEnd), ct);


	//public Task<(int Status, string Body)> GetByIdAsync(
	//	string id,
	//	string? licenseNumber = null,
	//	CancellationToken ct = default) => 
	//	_http.GetAsync(MetrcUrls.GetById(id, licenseNumber), ct);


	//public Task<(int Status, string Body)> PostDeliveriesAsync(
	//	string licenseNumber,
	//	string rawJsonArrayBody,
	//	CancellationToken ct = default) 
	//	=> _http.PostAsync(MetrcUrls.PostDeliveries(licenseNumber), rawJsonArrayBody, ct);
}
