using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Catalogi;

public record ZgwStatustype
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("omschrijving")]
    public required string Omschrijving { get; init; }

    [JsonPropertyName("omschrijvingGeneriek")]
    public string OmschrijvingGeneriek { get; init; } = string.Empty;
}
