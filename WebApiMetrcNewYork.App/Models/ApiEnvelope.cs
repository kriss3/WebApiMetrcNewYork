using System.Text.Json;

namespace WebApiMetrcNewYork.App.Models;

public sealed record ApiEnvelope(
	string Status,           // this would be either: "success" | "failure"
	int HttpCode,
	string Message,
	JsonElement? Data,
	DateTimeOffset ReceivedAtUtc)
{
	public static ApiEnvelope Success(int httpCode, JsonElement data) =>
		new("success", httpCode, "", data, DateTimeOffset.UtcNow);

	public static ApiEnvelope Failure(int httpCode, string message) =>
		new("failure", httpCode, message ?? "", null, DateTimeOffset.UtcNow);
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
				// ✅ On success: flatten Metrc's wrapper so ApiEnvelope.Data == content of "Data" (if present)
				var payload = ExtractMetrcPayload(root).Clone(); // clone to survive 'doc' disposal
				return ApiEnvelope.Success(code, payload);
			}

			// ❌ On failure: keep current best-effort message extraction; Data = null
			var msg = TryExtractErrorMessage(root) ?? resp.ReasonPhrase ?? "Request failed";
			return ApiEnvelope.Failure(code, msg);
		}
		catch
		{
			// Defensive fallback if body isn't valid JSON
			var msg = resp.IsSuccessStatusCode ? "" : (resp.ReasonPhrase ?? "Request failed");
			return resp.IsSuccessStatusCode
				? ApiEnvelope.Success(code, JsonDocument.Parse("{}").RootElement.Clone())
				: ApiEnvelope.Failure(code, msg);
		}
	}

	// --- helpers ---

	// Returns the content you actually want to expose as Data
	private static JsonElement ExtractMetrcPayload(JsonElement root)
	{
		// Typical Metrc success shape: { "Data": [...], "Meta": { ... } }
		if (root.ValueKind == JsonValueKind.Object)
		{
			// Prefer "Data" (Metrc), fall back to "data" if ever lower-cased
			if (root.TryGetProperty("Data", out var dataProp))
				return dataProp;
			if (root.TryGetProperty("data", out var dataPropLower))
				return dataPropLower;
		}

		// Some endpoints already return an array or object directly
		return root;
	}

	private static string? TryExtractErrorMessage(JsonElement root)
	{
		if (root.ValueKind == JsonValueKind.Object)
		{
			if (root.TryGetProperty("Message", out var m) && m.ValueKind == JsonValueKind.String) return m.GetString();
			if (root.TryGetProperty("message", out var mm) && mm.ValueKind == JsonValueKind.String) return mm.GetString();

			// Common Metrc error shape: { "Errors": [ "...", { "Message": "..." }, ... ] }
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
