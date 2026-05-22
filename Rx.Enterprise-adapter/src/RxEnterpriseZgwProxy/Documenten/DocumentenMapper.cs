using RxEnterprise.Client;

namespace RxEnterpriseZgwProxy.Documenten;

public static class DocumentenMapper
{
    public static ZgwEnkelvoudigInformatieObject ToEnkelvoudigInformatieObject(RxDocument doc, int virtualId, string selfUrl)
    {
        var idx = virtualId - 1;
        var item = doc.Bijlageinfo?.FirstOrDefault(x => x.Virtualid == virtualId);

        var filename = item?.Filename ?? string.Empty;
        var contentType = doc.Bijlagecontenttype?.ElementAtOrDefault(idx) ?? "application/octet-stream";
        var grootteStr = doc.Bijlagegrootte?.ElementAtOrDefault(idx);

        return new ZgwEnkelvoudigInformatieObject
        {
            Url = selfUrl,
            Identificatie = doc.Documentnummer ?? string.Empty,
            Bronorganisatie = string.Empty,
            Creatiedatum = doc.Creatiedatum ?? string.Empty,
            Titel = doc.Bijlageomschrijving?.ElementAtOrDefault(idx) ?? filename,
            Vertrouwelijkheidaanduiding = "openbaar",
            Formaat = contentType,
            Bestandsnaam = filename,
            Bestandsomvang = int.TryParse(grootteStr, out var size) ? size : 0,
            Inhoud = $"{selfUrl}/download",
        };
    }

    public static string? FindAttachmentFilename(RxDocument doc, int virtualId) =>
        doc.Bijlageinfo?.FirstOrDefault(x => x.Virtualid == virtualId)?.Filename;
}
