using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwZaakInformatieObject
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("uuid")]
    public required string Uuid { get; init; }

    [JsonPropertyName("informatieobject")]
    public required string Informatieobject { get; init; }

    [JsonPropertyName("zaak")]
    public required string Zaak { get; init; }

    [JsonPropertyName("aardRelatieWeergave")]
    public required string AardRelatieWeergave { get; init; }

    [JsonPropertyName("titel")]
    public required string Titel { get; init; }

    [JsonPropertyName("beschrijving")]
    public required string Beschrijving { get; init; }

    [JsonPropertyName("registratiedatum")]
    public required string Registratiedatum { get; init; }

    [JsonPropertyName("vernietigingsdatum")]
    public string? Vernietigingsdatum { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
