using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Catalogi;

public record ZgwZaaktype
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("omschrijving")]
    public required string Omschrijving { get; init; }
}
