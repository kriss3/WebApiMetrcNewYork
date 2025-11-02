namespace WebApiMetrcNewYork.App.Client;

public class MetrcUrls
{
	private const string Host = "https://api-ny.metrc.com"; // absolute host as requested

	// POST /sales/v2/deliveries?licenseNumber=...
	public static string PostDeliveries(string licenseNumber) => 
		$"{Host}/sales/v2/deliveries?licenseNumber={Uri.EscapeDataString(licenseNumber)}";

	// GET /sales/v2/deliveries/active?licenseNumber=...&pageNumber=...&pageSize=...&salesDateStart=...&salesDateEnd=...&lastModifiedStart=...&lastModifiedEnd=...
	// All params are strings (already formatted). Pass null/empty to omit.
	public static string GetActive(
	string licenseNumber,
	string? pageNumber = null,
	string? pageSize = null,
	string? salesDateStart = null,
	string? salesDateEnd = null,
	string? lastModifiedStart = null,
	string? lastModifiedEnd = null)
	{
		var q = new List<string> { $"licenseNumber={Uri.EscapeDataString(licenseNumber)}" };
		if (!string.IsNullOrWhiteSpace(pageNumber)) 
			q.Add($"pageNumber={Uri.EscapeDataString(pageNumber)}");
		
		if (!string.IsNullOrWhiteSpace(pageSize)) 
			q.Add($"pageSize={Uri.EscapeDataString(pageSize)}");

		if (!string.IsNullOrWhiteSpace(salesDateStart)) 
			q.Add($"salesDateStart={Uri.EscapeDataString(salesDateStart)}");
		if (!string.IsNullOrWhiteSpace(salesDateEnd)) 
			q.Add($"salesDateEnd={Uri.EscapeDataString(salesDateEnd)}");
		
		if (!string.IsNullOrWhiteSpace(lastModifiedStart)) 
			q.Add($"lastModifiedStart={Uri.EscapeDataString(lastModifiedStart)}");

		if (!string.IsNullOrWhiteSpace(lastModifiedEnd)) 
			q.Add($"lastModifiedEnd={Uri.EscapeDataString(lastModifiedEnd)}");
		
		var query = string.Join("&", q);
		return $"{Host}/sales/v2/deliveries/active?{query}";
	}

	// GET /sales/v2/deliveries/{id}[?licenseNumber=...]
	public static string GetById(string id, string? licenseNumber = null) 	=> 
		string.IsNullOrWhiteSpace(licenseNumber)
			? $"{Host}/sales/v2/deliveries/{Uri.EscapeDataString(id)}"
			: $"{Host}/sales/v2/deliveries/{Uri.EscapeDataString(id)}?licenseNumber={Uri.EscapeDataString(licenseNumber)}";
}
