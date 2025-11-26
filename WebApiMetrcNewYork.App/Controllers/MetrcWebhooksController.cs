using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Text.Json;
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

	[HttpPost("packages/subscribe")]
	public async Task<IActionResult> SubscribeToPackages(CancellationToken ct) 
	{
		throw new NotImplementedException();
	}
}
