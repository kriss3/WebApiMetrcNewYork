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

	public Task<ApiEnvelope> GetActiveAsync(PackagesActiveQuery q, CancellationToken ct)
	{
		var dict = new Dictionary<string, string?>
		{
			["licenseNumber"] = _opts.LicenseNumber
		};

		if (q.LastModifiedStartDate is { } s)
			dict["lastModifiedStart"] = s.ToString("yyyy-MM-dd");
		if (q.LastModifiedEndDate is { } e)
			dict["lastModifiedEnd"] = e.ToString("yyyy-MM-dd");

		if (q.PageNumber is { } p) dict["pageNumber"] = p.ToString();
		if (q.PageSize is { } z) dict["pageSize"] = z.ToString();

		var url = QueryHelpers.AddQueryString("/packages/v2/active", dict);
		return _http.GetAsync(url, ct);
	}

	public Task<ApiEnvelope> GetByLabelAsync(string label, CancellationToken ct)
	{
		var qs = $"licenseNumber={HttpUtility.UrlEncode(_opts.LicenseNumber)}";
		var url = $"/packages/v2/{HttpUtility.UrlEncode(label)}?{qs}";
		return _http.GetAsync(url, ct);
	}
}
