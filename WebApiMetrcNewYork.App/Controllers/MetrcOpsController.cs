using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;

[ApiController]
[Route("api/ops/metrc")]
public sealed class MetrcOpsController(IMetrcDeliveriesService svc) : ControllerBase
{
	private readonly IMetrcDeliveriesService _svc = svc;
	
}
