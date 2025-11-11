namespace WebApiMetrcNewYork.App.Models;

public sealed class MetrcOptions
{
	public const string SectionName = "MetrcSandbox";

	public string BaseUrl { get; init; } = "https://sandbox-api-ny.metrc.com/";
	public string VendorApiKey { get; init; } = default!;
	public string UserApiKey { get; init; } = default!;
	public string LicenseNumber { get; init; } = default!;
}