using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

public record RxZaaktype
{
    [JsonPropertyName("sleutel")]
    public string? Sleutel { get; init; }

    [JsonPropertyName("onderwerp")]
    public string? Onderwerp { get; init; }
}
