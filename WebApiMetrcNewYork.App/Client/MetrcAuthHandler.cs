using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Client;

public sealed class MetrcAuthHandler(IOptions<MetrcOptions> opts) : DelegatingHandler
{
	private readonly MetrcOptions _opts = opts.Value;

	protected override Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request, 
		CancellationToken ct)
	{
		var token = Convert.ToBase64String(
			Encoding.ASCII.GetBytes($"{_opts.VendorApiKey}:{_opts.UserApiKey}"));
		request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);
		request.Headers.Accept.ParseAdd("application/json");
		
		return base.SendAsync(request, ct);
	}
}
