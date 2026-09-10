using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Zaken;

public record ZgwNatuurlijkPersoon
{
    [JsonPropertyName("inpBsn")]
    public string InpBsn { get; init; } = string.Empty;

    [JsonPropertyName("anpIdentificatie")]
    public string AnpIdentificatie { get; init; } = string.Empty;

    [JsonPropertyName("inpA_nummer")]
    public string InpANummer { get; init; } = string.Empty;

    [JsonPropertyName("geslachtsnaam")]
    public string Geslachtsnaam { get; init; } = string.Empty;

    [JsonPropertyName("voorvoegselGeslachtsnaam")]
    public string VoorvoegselGeslachtsnaam { get; init; } = string.Empty;

    [JsonPropertyName("voorletters")]
    public string Voorletters { get; init; } = string.Empty;

    [JsonPropertyName("voornamen")]
    public string Voornamen { get; init; } = string.Empty;

    [JsonPropertyName("geslachtsaanduiding")]
    public string Geslachtsaanduiding { get; init; } = string.Empty;

    [JsonPropertyName("geboortedatum")]
    public string Geboortedatum { get; init; } = string.Empty;

    [JsonPropertyName("verblijfsadres")]
    public object? Verblijfsadres { get; init; }

    [JsonPropertyName("subVerblijfBuitenland")]
    public object? SubVerblijfBuitenland { get; init; }
}
