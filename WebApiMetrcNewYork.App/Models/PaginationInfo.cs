namespace WebApiMetrcNewYork.App.Models;

public sealed record PaginationInfo(
	int? Total,
	int? TotalRecords,
	int? PageSize,
	int? RecordsOnPage,
	int? Page,
	int? CurrentPage,
	int? TotalPages
);
