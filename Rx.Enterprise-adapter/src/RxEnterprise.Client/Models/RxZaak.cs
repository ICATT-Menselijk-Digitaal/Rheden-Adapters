using System.Text.Json;
using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

public record RxZaak
{
    [JsonPropertyName("sleutel")]
    public string? Sleutel { get; init; }

    [JsonPropertyName("betreft")]
    public string? Betreft { get; init; }

    [JsonPropertyName("onderwerp")]
    public string? Onderwerp { get; init; }

    [JsonPropertyName("boekdatum")]
    public string? Boekdatum { get; init; }

    [JsonPropertyName("afhandelingsstatus")]
    public string? Afhandelingsstatus { get; init; }

    [JsonPropertyName("documentnummer")]
    public string? Documentnummer { get; init; }

    [JsonPropertyName("zaaktypesleutel")]
    public string? Zaaktypesleutel { get; init; }

    [JsonPropertyName("afzender")]
    public string? Afzender { get; init; }

    [JsonPropertyName("voorlettersafzender")]
    public string? Voorlettersafzender { get; init; }

    [JsonPropertyName("voornamenafzender")]
    public string? Voornamenafzender { get; init; }

    [JsonPropertyName("voorvoegselafzender")]
    public string? Voorvoegselafzender { get; init; }

    [JsonPropertyName("eerstebehandelaar")]
    public List<string>? Eerstebehandelaar { get; init; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; init; }
}
