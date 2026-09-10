using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwRol
{
    [JsonPropertyName("betrokkeneType")]
    public required string BetrokkeneType { get; init; }

    [JsonPropertyName("omschrijving")]
    public required string Omschrijving { get; init; }

    [JsonPropertyName("omschrijvingGeneriek")]
    public required string OmschrijvingGeneriek { get; init; }

    [JsonPropertyName("betrokkeneIdentificatie")]
    public required ZgwNatuurlijkPersoon BetrokkeneIdentificatie { get; init; }
}
