using Microsoft.Extensions.Options;
using System.Net;
using System.Web;
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;


/// <summary>
/// Makes outbound calls to Metrc Sandbox for Deliveries endpoints.
/// Uses the named HttpClient "MetrcNY" configured in Program.cs.
/// </summary>
public sealed class MetrcDeliveriesSandboxService : IMetrcDeliveriesSandboxService
{
	private readonly IMetrcHttp _http;
	private readonly MetrcOptions _opts;

	public MetrcDeliveriesSandboxService(IMetrcHttp http, IOptions<MetrcOptions> opts)
	{
		_http = http;
		_opts = opts.Value;
	}

	public async Task<(HttpStatusCode status, string json)> GetActiveAsync(CancellationToken ct)
	{
		var query = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/sales/v2/deliveries/active?{query}";

		var resp = await _http.GetAsync(url, ct);
		var json = await resp.Content.ReadAsStringAsync(ct);
		return (resp.StatusCode, json);
	}
}
