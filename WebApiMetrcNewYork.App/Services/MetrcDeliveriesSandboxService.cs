using Microsoft.Extensions.Options;
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcDeliveriesSandboxService : IMetrcDeliveriesSandboxService
{
	private readonly IMetrcHttp _http;
	private readonly MetrcOptions _opts;

	public MetrcDeliveriesSandboxService(IMetrcHttp http, IOptions<MetrcOptions> opts)
	{
		_http = http;
		_opts = opts.Value;
	}
}
