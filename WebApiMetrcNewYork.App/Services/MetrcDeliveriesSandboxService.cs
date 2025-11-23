using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Web;
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;


/// <summary>
/// Makes outbound calls to Metrc Sandbox for Deliveries endpoints.
/// Uses the named HttpClient "MetrcNY" configured in Program.cs.
/// </summary>
public sealed class MetrcDeliveriesSandboxService(
	IMetrcHttp http, 
	IOptions<MetrcOptions> opts) : IMetrcDeliveriesSandboxService
{
	private readonly IMetrcHttp _http = http;
	private readonly MetrcOptions _opts = opts.Value;

	public Task<ApiEnvelope> GetActiveAsync(CommonQueryParams query, CancellationToken ct)
	{
		var dict = new Dictionary<string, string?>
		{
			["licenseNumber"] = _opts.LicenseNumber
		};

		if (query.LastModifiedStart is { } s)
			dict["lastModifiedStart"] = s.ToString("yyyy-MM-dd");
		if (query.LastModifiedEnd is { } e)
			dict["lastModifiedEnd"] = e.ToString("yyyy-MM-dd");

		if (query.PageNumber is { } p) dict["pageNumber"] = p.ToString();
		if (query.PageSize is { } z) dict["pageSize"] = z.ToString();

		var url = QueryHelpers.AddQueryString("/sales/v2/deliveries/active", dict);
		return _http.GetAsync(url, ct);
	}

	public Task<ApiEnvelope> GetActiveByIdAsync(int deliveryId, CancellationToken ct)
	{
		throw new NotImplementedException();
	}

	public Task<ApiEnvelope> CreateAsync(object payload, CancellationToken ct)
	{
		var query = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/sales/v2/deliveries?{query}";

		var jsonBody = System.Text.Json.JsonSerializer.Serialize(payload);
		return _http.PostAsync(url, jsonBody, ct);
	}
}
