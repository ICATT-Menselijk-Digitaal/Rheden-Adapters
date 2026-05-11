namespace RxEnterprise.Client;

internal sealed class RxEnterpriseClient(HttpClient httpClient) : IRxEnterpriseClient
{
    public async Task<string> GetZaakAsync(string zaakId, CancellationToken ct = default)
    {
        var response = await httpClient.GetAsync($"data/zaak/{zaakId}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(ct);
    }

    public async Task<string> SearchZaakAsync(string query, CancellationToken ct = default)
    {
        var response = await httpClient.GetAsync($"data/zaak?search={Uri.EscapeDataString(query)}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(ct);
    }
}
