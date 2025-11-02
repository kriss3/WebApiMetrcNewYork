using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;

[ApiController]
[Route("api/ops/metrc")]
public sealed class MetrcOpsController(IMetrcDeliveriesService svc) : ControllerBase
{
	private readonly IMetrcDeliveriesService _svc = svc;

	// GET /api/ops/metrc/deliveries/active?license=...&pageNumber=...&pageSize=...&lastModifiedStart=...&lastModifiedEnd=...
	[HttpGet("deliveries/active")]
	public async Task<IActionResult> GetActive(
	[FromQuery] string license,
	[FromQuery] string? pageNumber,
	[FromQuery] string? pageSize,
	[FromQuery] string? salesDateStart,
	[FromQuery] string? salesDateEnd,
	[FromQuery] string? lastModifiedStart,
	[FromQuery] string? lastModifiedEnd,
	CancellationToken ct)
	{
		var (status, body) = await _svc.GetActiveAsync(license, pageNumber, pageSize, salesDateStart, salesDateEnd, lastModifiedStart, lastModifiedEnd, ct);
		return StatusCode(status, new { status, body });
	}



}
