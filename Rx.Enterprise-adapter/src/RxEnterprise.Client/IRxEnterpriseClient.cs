namespace RxEnterprise.Client;

public interface IRxEnterpriseClient
{
    Task<string> GetZaakAsync(string zaakId, CancellationToken ct = default);
    Task<string> SearchZaakAsync(string query, CancellationToken ct = default);
}
