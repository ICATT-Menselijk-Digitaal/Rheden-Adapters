using System.Text.Json;
using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

public record RxDocument
{
    [JsonPropertyName("documentnummer")]
    public string? Documentnummer { get; init; }

    [JsonPropertyName("creatiedatum")]
    public string? Creatiedatum { get; init; }

    [JsonPropertyName("bijlageinfo")]
    public List<RxBijlageInfo>? Bijlageinfo { get; init; }

    // Parallel arrays indexed by bijlageinfo position (virtualid - 1)
    [JsonPropertyName("bijlagedatumtijd")]
    public List<string?>? Bijlagedatumtijd { get; init; }

    [JsonPropertyName("bijlageomschrijving")]
    public List<string?>? Bijlageomschrijving { get; init; }

    [JsonPropertyName("bijlagecontenttype")]
    public List<string?>? Bijlagecontenttype { get; init; }

    [JsonPropertyName("bijlagegrootte")]
    public List<string?>? Bijlagegrootte { get; init; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Extra { get; init; }
}

public record RxBijlageInfo
{
    [JsonPropertyName("virtualid")]
    public int Virtualid { get; init; }

    [JsonPropertyName("filename")]
    public string? Filename { get; init; }
}
