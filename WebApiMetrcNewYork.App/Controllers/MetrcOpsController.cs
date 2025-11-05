using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;

[ApiController]
[Route("api/ops/metrc")]
public sealed class MetrcOpsController(IMetrcDeliveriesService svc) : ControllerBase
{
	private readonly IMetrcDeliveriesService _svc = svc;

	// GET /api/ops/metrc/deliveries/active?license=<params>
	[HttpGet("deliveries/active")]
	public async Task<IActionResult> GetActive([FromQuery] string license,
		[FromQuery] string? pageNumber, [FromQuery] string? pageSize,
		[FromQuery] string? salesDateStart, [FromQuery] string? salesDateEnd,
		[FromQuery] string? lastModifiedStart, [FromQuery] string? lastModifiedEnd,
		CancellationToken ct)
	{
		var env = await _svc.GetActiveAsync(
			license,
			pageNumber,
			pageSize,
			salesDateStart, 
			salesDateEnd,
			lastModifiedStart, 
			lastModifiedEnd, ct);
		return StatusCode(env.HttpCode, env);
	}

	[HttpGet("deliveries/{id}")]
	public async Task<IActionResult> GetById([FromRoute] string id, [FromQuery] string? license, CancellationToken ct)
	{
		var env = await _svc.GetByIdAsync(id, license, ct);
		return StatusCode(env.HttpCode, env);
	}




	// GET /api/ops/metrc/deliveries/active?license=<params>
	//[HttpGet("deliveries/active")]
	//public async Task<IActionResult> GetActive(
	//[FromQuery] string license,
	//[FromQuery] string? pageNumber,
	//[FromQuery] string? pageSize,
	//[FromQuery] string? salesDateStart,
	//[FromQuery] string? salesDateEnd,
	//[FromQuery] string? lastModifiedStart,
	//[FromQuery] string? lastModifiedEnd,
	//CancellationToken ct)
	//{
	//	var (status, body) = await _svc.GetActiveAsync(
	//		license,
	//		pageNumber,
	//		pageSize,
	//		salesDateStart,
	//		salesDateEnd,
	//		lastModifiedStart,
	//		lastModifiedEnd, 
	//		ct);
	//	return StatusCode(status, new { status, body });
	//}

	//// GET /api/ops/metrc/deliveries/{id}?license=<params>
	//[HttpGet("deliveries/{id}")]
	//public async Task<IActionResult> GetById(
	//	[FromRoute] string id,
	//	[FromQuery] string? license,
	//	CancellationToken ct)
	//{
	//	var (status, body) = await _svc.GetByIdAsync(id, license, ct);
	//	return StatusCode(status, new { status, body });
	//}

	//// POST /api/ops/metrc/deliveries?license=<params>
	//[HttpPost("deliveries")]
	//public async Task<IActionResult> PostDeliveries(
	//	[FromQuery] string license,
	//	[FromBody] string rawJsonArrayBody,
	//	CancellationToken ct)
	//{
	//	var (status, body) = await _svc.PostDeliveriesAsync(license, rawJsonArrayBody, ct);
	//	return StatusCode(status, new { status, body });
	//}
}
