using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwStatus
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("uuid")]
    public required string Uuid { get; init; }

    [JsonPropertyName("zaak")]
    public string? Zaak { get; init; }

    [JsonPropertyName("statustype")]
    public required string Statustype { get; init; }

    [JsonPropertyName("datumStatusGezet")]
    public string? DatumStatusGezet { get; init; }

    [JsonPropertyName("statustoelichting")]
    public required string Statustoelichting { get; init; }

    [JsonPropertyName("indicatieLaatstGezetteStatus")]
    public bool? IndicatieLaatstGezetteStatus { get; init; }
}
