using Microsoft.AspNetCore.Mvc;

namespace WebApiMetrcNewYork.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PingController : ControllerBase
{
	[HttpGet]
	public IActionResult Get() => Ok(new
	{
		message = "pong 🏓",
		timestamp = DateTime.UtcNow
	});
}