using WebApiMetrcNewYork.App.Client;
using WebApiMetrcNewYork.App.Models;

namespace WebApiMetrcNewYork.App.Services;

public sealed class MetrcPackagesService : IMetrcPackagesService
{
	private readonly IMetrcHttp _http;
	private readonly MetrcOptions _opts;

}
