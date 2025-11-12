using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class MetrcNyController(
    IMetrcPackagesService packages,
    IMetrcDeliveriesSandboxService deliveriesSandbox) : ControllerBase
{
	private readonly IMetrcPackagesService _packages = packages;
	private readonly IMetrcDeliveriesSandboxService _deliveriesSandbox = deliveriesSandbox;






}
