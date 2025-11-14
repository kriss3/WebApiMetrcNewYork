namespace WebApiMetrcNewYork.App.Models;

public sealed class PackagesActiveQuery
{
	// Use DateOnly to naturally express "date without time"
	public DateOnly? LastModifiedStart { get; init; }
	public DateOnly? LastModifiedEnd { get; init; }

	public int? PageNumber { get; init; }
	public int? PageSize { get; init; }
}
