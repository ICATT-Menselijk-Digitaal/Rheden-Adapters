using RxEnterprise.Client;

namespace RxEnterpriseZgwProxy.Documenten;

public static class DocumentenMapper
{
    public static ZgwEnkelvoudigInformatieObject ToEnkelvoudigInformatieObject(RxZaakDocument doc, string selfUrl) =>
        new()
        {
            Url = selfUrl,
            Identificatie = doc.Doelsleutel ?? string.Empty,
            Bronorganisatie = string.Empty,
            Creatiedatum = doc.Doeldocumentdatum ?? string.Empty,
            Titel = doc.Doeldocumenttitel ?? string.Empty,
            Vertrouwelijkheidaanduiding = string.Empty,
            Formaat = doc.Doelbijlagecontenttype?.FirstOrDefault() ?? string.Empty,
            Bestandsnaam = doc.Doelbijlagenaam?.FirstOrDefault() ?? string.Empty,
            Bestandsomvang = int.TryParse(doc.Doelbijlagegrootte?.FirstOrDefault(), out var size) ? size : 0,
            Inhoud = $"{selfUrl}/download",
        };

    public static string? GetDownloadUrl(RxZaakDocument doc)
    {
        var url = doc.Doelbijlageurl?.FirstOrDefault();
        if (!string.IsNullOrEmpty(url)) 
        {
            return url;
        }

        var sleutel = doc.Doelsleutel;
        var naam = doc.Doelbijlagenaam?.FirstOrDefault();
        if (!string.IsNullOrEmpty(sleutel) && !string.IsNullOrEmpty(naam)) 
        {
            return $"/data/document/{Uri.EscapeDataString(sleutel)}/{Uri.EscapeDataString(naam)}";
        }

        return null;
    }
}
