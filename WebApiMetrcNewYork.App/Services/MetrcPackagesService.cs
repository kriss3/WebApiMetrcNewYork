using Microsoft.Extensions.Options;
using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcPackagesService(IMetrcHttp http, IOptions<MetrcOptions> opts) 
	: IMetrcPackagesService
{
	private readonly IMetrcHttp _http = http;
	private readonly MetrcOptions _opts = opts.Value;
}
