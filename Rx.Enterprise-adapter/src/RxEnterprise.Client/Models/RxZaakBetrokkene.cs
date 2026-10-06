using System.Text.Json.Serialization;

namespace RxEnterprise.Client;

public record RxZaakBetrokkene
{
    [JsonPropertyName("bronsleutel")]
    public string? Bronsleutel { get; init; }

    [JsonPropertyName("bronbetreft")]
    public string? Bronbetreft { get; init; }

    [JsonPropertyName("brononderwerp")]
    public string? Brononderwerp { get; init; }

    [JsonPropertyName("bronstartdatum")]
    [JsonConverter(typeof(UnixMillisToDateConverter))]
    public string? Bronstartdatum { get; init; }

    [JsonPropertyName("bronboekdatum")]
    [JsonConverter(typeof(UnixMillisToDateConverter))]
    public string? Bronboekdatum { get; init; }

    [JsonPropertyName("bronafhandelingsstatus")]
    public string? Bronafhandelingsstatus { get; init; }

    [JsonPropertyName("bronzaaktypesleutel")]
    public string? Bronzaaktypesleutel { get; init; }

    [JsonPropertyName("context")]
    public string? Context { get; init; }
}
