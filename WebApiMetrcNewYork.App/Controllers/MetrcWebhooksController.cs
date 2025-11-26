using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMetrcNewYork.App.Services;

namespace WebApiMetrcNewYork.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetrcWebhooksController(IMetrcWebHooksService webhooksService) : ControllerBase
{
	private readonly IMetrcWebHooksService _webhooksService = webhooksService;
}
