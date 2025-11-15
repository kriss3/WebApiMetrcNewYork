using Microsoft.AspNetCore.WebUtilities;
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

	public Task<ApiEnvelope> GetActiveAsync(CommonQueryParams query, CancellationToken ct)
	{
		// License Information comes from MetrcOptions.
		// Query parameters come from CommonQueryParams.
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

		var url = QueryHelpers.AddQueryString("/packages/v2/active", dict);
		return _http.GetAsync(url, ct);
	}

	public Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct)
	{
		var url = QueryHelpers.AddQueryString($"/packages/v2/{Uri.EscapeDataString(label)}",
			new Dictionary<string, string?> { ["licenseNumber"] = _opts.LicenseNumber });

		return _http.GetAsync(url, ct);
	}
}
