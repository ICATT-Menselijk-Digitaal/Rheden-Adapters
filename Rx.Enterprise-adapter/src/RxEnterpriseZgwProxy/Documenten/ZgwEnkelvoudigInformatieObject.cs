using System.Text.Json.Serialization;

namespace RxEnterpriseZgwProxy.Documenten;

public record ZgwEnkelvoudigInformatieObject
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("identificatie")]
    public required string Identificatie { get; init; }

    [JsonPropertyName("bronorganisatie")]
    public required string Bronorganisatie { get; init; }

    [JsonPropertyName("creatiedatum")]
    public required string Creatiedatum { get; init; }

    [JsonPropertyName("titel")]
    public required string Titel { get; init; }

    [JsonPropertyName("vertrouwelijkheidaanduiding")]
    public required string Vertrouwelijkheidaanduiding { get; init; }

    [JsonPropertyName("formaat")]
    public required string Formaat { get; init; }

    [JsonPropertyName("bestandsnaam")]
    public required string Bestandsnaam { get; init; }

    [JsonPropertyName("bestandsomvang")]
    public required int Bestandsomvang { get; init; }

    [JsonPropertyName("inhoud")]
    public required string Inhoud { get; init; }
}
