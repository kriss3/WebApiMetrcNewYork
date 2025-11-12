using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Models;
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

	// ---------------- Packages ----------------

	// GET /api/metrcny/packages/active?lastModifiedStart=...&lastModifiedEnd=...
	[HttpGet("packages/active")]
	public async Task<IActionResult> GetActivePackages(
		[FromQuery] DateTimeOffset? lastModifiedStart,
		[FromQuery] DateTimeOffset? lastModifiedEnd,
		CancellationToken ct)
	{
		ApiEnvelope env = await _packages.GetActiveAsync(lastModifiedStart, lastModifiedEnd, ct);
		return StatusCode(env.HttpCode, env);
	}

	// GET /api/metrcny/packages/{label}
	[HttpGet("packages/{label}")]
	public async Task<IActionResult> GetPackageByLabel([FromRoute] string label, CancellationToken ct)
	{
		ApiEnvelope env = await _packages.GetByLabelAsync(label, ct);
		return StatusCode(env.HttpCode, env);
	}




}
