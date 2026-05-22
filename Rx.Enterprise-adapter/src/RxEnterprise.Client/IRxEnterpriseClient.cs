namespace RxEnterprise.Client;

public interface IRxEnterpriseClient
{
    Task<IEnumerable<RxZaak>> SearchZaakAsync(string query, CancellationToken ct = default);
    Task<RxZaak> GetZaakAsync(string zaakId, CancellationToken ct = default);
    Task<RxDocument> GetDocumentAsync(string documentId, CancellationToken ct = default);
    Task<RxZaaktype> GetZaaktypeAsync(string sleutel, CancellationToken ct = default);
    Task<(Stream Content, string ContentType, string FileName)> DownloadDocumentAsync(string documentId, string fileName, CancellationToken ct = default);
}
