using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwZaak
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("uuid")]
    public required string Uuid { get; init; }

    [JsonPropertyName("identificatie")]
    public required string Identificatie { get; init; }

    [JsonPropertyName("omschrijving")]
    public required string Omschrijving { get; init; }

    [JsonPropertyName("bronorganisatie")]
    public required string Bronorganisatie { get; init; }

    [JsonPropertyName("zaaktype")]
    public required string Zaaktype { get; init; }

    [JsonPropertyName("registratiedatum")]
    public required string Registratiedatum { get; init; }

    [JsonPropertyName("startdatum")]
    public required string Startdatum { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("toelichting")]
    public required string Toelichting { get; init; }
}
