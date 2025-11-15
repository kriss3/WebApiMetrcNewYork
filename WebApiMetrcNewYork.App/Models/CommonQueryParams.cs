using System.ComponentModel.DataAnnotations;

namespace WebApiMetrcNewYork.App.Models;

public sealed class CommonQueryParams
{
	[Required]
	public string LicenseNumber { get; init; } = default!;
	
	// Use DateOnly to naturally express "date without time"
	public DateOnly? LastModifiedStart { get; init; }
	public DateOnly? LastModifiedEnd { get; init; }
	public int? PageNumber { get; init; }
	[Range(1, 20)]
	public int? PageSize { get; init; }
}
