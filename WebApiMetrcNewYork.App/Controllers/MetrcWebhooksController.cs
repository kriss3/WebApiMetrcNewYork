using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text.Json;
using WebApiMetrcNewYork.App.Models;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetrcWebhooksController(IMetrcWebHooksService webhooksService) : ControllerBase
{
	private readonly IMetrcWebHooksService _webhooksService = webhooksService;

	// Simple in-memory buffer just for proving that webhooks are firing.
	// For production, persist these via EF Core or queue them to Service Bus.
	private static readonly ConcurrentQueue<JsonElement> _receivedPackageEvents = new();

	// POST /api/metrc-webhooks/packages/subscribe
	// Registers a Package webhook subscription via Metrc Connect.
	[HttpPost("packages/subscribe")]
	public async Task<IActionResult> SubscribeToPackages(CancellationToken ct) 
	{
		ApiEnvelope env = await _webhooksService.SubscribeToPackageWebhooksAsync(ct);
		return StatusCode(env.HttpCode, env);
	}

	// GET /api/metrc-webhooks/subscriptions
	// Optional helper to see what Metrc thinks the subscriptions are.
	[HttpGet("subscriptions")]
	public async Task<IActionResult> GetSubscriptions(CancellationToken ct) 
	{
		ApiEnvelope env = await _webhooksService.GetSubscriptionsAsync(ct);
		return StatusCode(env.HttpCode, env);
	}

	// POST /api/metrc-webhooks/packages/inbound
	// This is the callback URL Metrc Connect will call when Packages change.
	// It must match the 'url' field in the subscription body.
	[HttpPost("packages/inbound")]
	public IActionResult ReceivePackageWebhook([FromBody] JsonElement body) 
	{
		_receivedPackageEvents.Enqueue(body);

		var dataCount = body.TryGetProperty("datacount", out var dc)
			? dc.GetInt32()
			: (int?)null;

		return Ok(new
		{
			message = "Webhook received",
			receivedAtUtc = DateTimeOffset.UtcNow,
			dataCount
		});
	}
}
