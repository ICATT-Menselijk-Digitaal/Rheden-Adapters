using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

public record RxZaakDocument
{
    [JsonPropertyName("doelsleutel")]
    public string? Doelsleutel { get; init; }

    [JsonPropertyName("doeldocumentdatum")]
    [JsonConverter(typeof(UnixMillisToDateConverter))]
    public string? Doeldocumentdatum { get; init; }

    [JsonPropertyName("doeldocumenttitel")]
    public string? Doeldocumenttitel { get; init; }

    [JsonPropertyName("doelbijlagecontenttype")]
    public List<string?>? Doelbijlagecontenttype { get; init; }

    [JsonPropertyName("doelbijlagenaam")]
    public List<string?>? Doelbijlagenaam { get; init; }

    [JsonPropertyName("doelbijlagegrootte")]
    public List<string?>? Doelbijlagegrootte { get; init; }

    [JsonPropertyName("doelbijlagedatumtijd")]
    public List<long>? Doelbijlagedatumtijd { get; init; }

    [JsonPropertyName("doelbijlageurl")]
    public List<string>? Doelbijlageurl { get; init; }

    [JsonPropertyName("doeldocunid")]
    public string? Doeldocunid { get; init; }
}
