namespace WebApiMetrcNewYork.App.Models;

public sealed class PackagesActiveQuery
{
	// Use DateOnly to naturally express "date without time"
	public DateOnly? LastModifiedStartDate { get; init; }
	public DateOnly? LastModifiedEndDate { get; init; }

	public int? PageNumber { get; init; }
	public int? PageSize { get; init; }
}
