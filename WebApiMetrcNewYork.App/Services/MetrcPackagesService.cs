using Microsoft.Extensions.Options;
using System.Globalization;
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

	public Task<ApiEnvelope> GetActiveAsync(
		DateTimeOffset? lastModifiedStart,
		DateTimeOffset? lastModifiedEnd,
		CancellationToken ct)
	{
		var qs = HttpUtility.ParseQueryString(string.Empty);
		qs["licenseNumber"] = _opts.LicenseNumber;

		// Use round-trippable ISO 8601 if provided
		if (lastModifiedStart.HasValue)
			qs["lastModifiedStart"] = lastModifiedStart.Value.UtcDateTime.ToString("o", CultureInfo.InvariantCulture);
		if (lastModifiedEnd.HasValue)
			qs["lastModifiedEnd"] = lastModifiedEnd.Value.UtcDateTime.ToString("o", CultureInfo.InvariantCulture);

		// Relative URL is fine because HttpClient BaseAddress is set
		var url = $"/packages/v2/active?{qs}";
		return _http.GetAsync(url, ct);
	}

	public Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct)
	{
		var qs = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/packages/v2/{HttpUtility.UrlEncode(label)}?{qs}";
		return _http.GetAsync(url, ct);
	}
}
