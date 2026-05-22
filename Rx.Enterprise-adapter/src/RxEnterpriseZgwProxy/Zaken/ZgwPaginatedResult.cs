using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwPaginatedResult<T>
{
    [JsonPropertyName("count")]
    public required int Count { get; init; }

    [JsonPropertyName("next")]
    public string? Next { get; init; }

    [JsonPropertyName("previous")]
    public string? Previous { get; init; }

    [JsonPropertyName("results")]
    public required IReadOnlyList<T> Results { get; init; }
}
