using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
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

	public Task<ApiEnvelope> GetActiveAsync(CancellationToken ct)
	{
		var query = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/sales/v2/deliveries/active?{query}";
		return _http.GetAsync(url, ct);
	}

	public Task<ApiEnvelope> CreateAsync(object payload, CancellationToken ct)
	{
		var query = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/sales/v2/deliveries?{query}";

		var jsonBody = System.Text.Json.JsonSerializer.Serialize(payload);
		return _http.PostAsync(url, jsonBody, ct);
	}
}
