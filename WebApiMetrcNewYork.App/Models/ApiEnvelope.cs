using System.Text.Json;

namespace WebApiMetrcNewYork.App.Models;

public sealed record ApiEnvelope(
	string Status,									// "success" | "failure"
	int HttpCode,
	string Message,
	JsonElement? Data,
	DateTimeOffset ReceivedAtUtc,
	JsonElement? Meta,						// raw Meta (if present)
	PaginationInfo? Pagination				// parsed paging info (if present)
)
{
	public static ApiEnvelope Success(int httpCode, JsonElement data, JsonElement? meta, PaginationInfo? pagination) =>
		new("success", httpCode, "", data, DateTimeOffset.UtcNow, meta, pagination);

	public static ApiEnvelope Failure(int httpCode, string message) =>
		new("failure", httpCode, message ?? "", null, DateTimeOffset.UtcNow, null, null);
}

public static class MetrcEnvelopeFactory
{
	public static async Task<ApiEnvelope> FromResponseAsync(HttpResponseMessage resp, CancellationToken ct)
	{
		var code = (int)resp.StatusCode;
		var text = await resp.Content.ReadAsStringAsync(ct);

		try
		{
			using var doc = JsonDocument.Parse(string.IsNullOrWhiteSpace(text) ? "{}" : text);
			var root = doc.RootElement;

			if (resp.IsSuccessStatusCode)
			{
				var (payload, metaNode) = ExtractPayloadAndMeta(root);
				var pagination = ParsePagination(root, metaNode);

				// clone elements because 'doc' will be disposed
				var payloadCloned = payload.Clone();
				JsonElement? metaCloned = metaNode.HasValue ? metaNode.Value.Clone() : (JsonElement?)null;

				return ApiEnvelope.Success(code, payloadCloned, metaCloned, pagination);
			}

			var msg = TryExtractErrorMessage(root) ?? resp.ReasonPhrase ?? "Request failed";
			return ApiEnvelope.Failure(code, msg);
		}
		catch
		{
			var msg = resp.IsSuccessStatusCode ? "" : (resp.ReasonPhrase ?? "Request failed");
			return resp.IsSuccessStatusCode
				? ApiEnvelope.Success(code, JsonDocument.Parse("{}").RootElement.Clone(), null, null)
				: ApiEnvelope.Failure(code, msg);
		}
	}

	private static (JsonElement payload, JsonElement? meta) ExtractPayloadAndMeta(JsonElement root)
	{
		JsonElement? meta = null;

		if (root.ValueKind == JsonValueKind.Object)
		{
			if (root.TryGetProperty("Meta", out var metaProp))
				meta = metaProp;

			if (root.TryGetProperty("Data", out var dataProp))
				return (dataProp, meta);

			if (root.TryGetProperty("data", out var dataPropLower))
				return (dataPropLower, meta);
		}

		// Some endpoints already return an array/object at the root
		return (root, meta);
	}

	private static PaginationInfo? ParsePagination(JsonElement root, JsonElement? meta)
	{
		// Try read from Meta first…
		var source = meta ?? (root.ValueKind == JsonValueKind.Object ? root : (JsonElement?)null);
		if (!source.HasValue || source.Value.ValueKind != JsonValueKind.Object) return null;

		int? ReadInt(string name)
		{
			return source.Value.TryGetProperty(name, out var el) && el.ValueKind == JsonValueKind.Number
				? el.TryGetInt32(out var i) ? i : (int?)null
				: (int?)null;
		}

		// Support both locations (top-level or under "Meta") and both name variants often seen
		var total = ReadInt("Total") ?? ReadInt("total");
		var totalRecords = ReadInt("TotalRecords") ?? ReadInt("totalRecords");
		var pageSize = ReadInt("PageSize") ?? ReadInt("pageSize");
		var recordsOnPage = ReadInt("RecordsOnPage") ?? ReadInt("recordsOnPage");
		var page = ReadInt("Page") ?? ReadInt("page");
		var currentPage = ReadInt("CurrentPage") ?? ReadInt("currentPage");
		var totalPages = ReadInt("TotalPages") ?? ReadInt("totalPages");

		// If none present, return null
		if (total is null && totalRecords is null && pageSize is null &&
			recordsOnPage is null && page is null && currentPage is null && totalPages is null)
			return null;

		return new PaginationInfo(total, totalRecords, pageSize, recordsOnPage, page, currentPage, totalPages);
	}

	private static string? TryExtractErrorMessage(JsonElement root)
	{
		if (root.ValueKind == JsonValueKind.Object)
		{
			if (root.TryGetProperty("Message", out var m) && m.ValueKind == JsonValueKind.String) return m.GetString();
			if (root.TryGetProperty("message", out var mm) && mm.ValueKind == JsonValueKind.String) return mm.GetString();

			if (root.TryGetProperty("Errors", out var errs) &&
				errs.ValueKind == JsonValueKind.Array && errs.GetArrayLength() > 0)
			{
				var first = errs[0];
				if (first.ValueKind == JsonValueKind.String) return first.GetString();
				if (first.ValueKind == JsonValueKind.Object &&
					first.TryGetProperty("Message", out var em) &&
					em.ValueKind == JsonValueKind.String)
					return em.GetString();
			}
		}
		return null;
	}
}
