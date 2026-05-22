using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Catalogi;

public record ZgwZaaktype
{
    [JsonPropertyName("identificatie")]
    public required string Identificatie { get; init; }

    [JsonPropertyName("omschrijving")]
    public required string Omschrijving { get; init; }
}
