namespace WebApiMetrcNewYork.App.Models;

public class MetrcOptions
{
	public string BaseUrl { get; init; } = "https://api-ny.metrc.com";
	public required string VendorApiKey { get; init; }
	public required string UserApiKey { get; init; }
}
