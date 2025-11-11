using Microsoft.Extensions.Options;
using System.Net;
using System.Web;
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcPackagesService(IMetrcHttp http, IOptions<MetrcOptions> opts) 
	: IMetrcPackagesService
{
	private readonly IMetrcHttp _http = http;
	private readonly MetrcOptions _opts = opts.Value;

	public async Task<(HttpStatusCode status, string json)> GetActiveAsync(
		DateTimeOffset? lastModifiedStart, DateTimeOffset? lastModifiedEnd, CancellationToken ct)
	{
		var qs = HttpUtility.ParseQueryString(string.Empty);
		qs["licenseNumber"] = _opts.LicenseNumber;
		if (lastModifiedStart.HasValue) qs["lastModifiedStart"] = lastModifiedStart.Value.UtcDateTime.ToString("o");
		if (lastModifiedEnd.HasValue) qs["lastModifiedEnd"] = lastModifiedEnd.Value.UtcDateTime.ToString("o");

		var url = $"/packages/v2/active?{qs}";
		var resp = await _http.GetAsync(url, ct);
		return (resp.StatusCode, await resp.Content.ReadAsStringAsync(ct));
	}

	public async Task<(HttpStatusCode status, string json)> GetByLabelAsync(string label, CancellationToken ct)
	{
		var qs = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/packages/v2/{HttpUtility.UrlEncode(label)}?{qs}";
		var resp = await _http.GetAsync(url, ct);
		return (resp.StatusCode, await resp.Content.ReadAsStringAsync(ct));
	}
}
