using RxEnterprise.Client;

namespace RxEnterpriseZgwProxy.Documenten;

public static class DocumentenEndpoints
{
    public static IEndpointRouteBuilder MapDocumentenEndpoints(this IEndpointRouteBuilder app)
    {
        // [JsonExtensionData] on RxDocument preserves all fields not in the typed model.
        app.MapGet("/documenten/api/v1/documenten/{id}", async (
            string id,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var doc = await rxClient.GetDocumentAsync(id, ct);
            return Results.Ok(doc);
        });

        // Download document binary — filename carries the extension
        app.MapGet("/documenten/api/v1/documenten/{id}/{filename}", async (
            string id,
            string filename,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var (stream, contentType, resolvedName) = await rxClient.DownloadDocumentAsync(id, filename, ct);
            return Results.Stream(stream, contentType, resolvedName);
        });

        // id is doelsleutel from zaak-document/search
        app.MapGet("/documenten/api/v1/enkelvoudiginformatieobjecten/{id}", async (
            string id,
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var selfUrl = $"{baseUrl}/documenten/api/v1/enkelvoudiginformatieobjecten/{Uri.EscapeDataString(id)}";

            var doc = await rxClient.GetZaakDocumentAsync(id, ct);
            if (doc is null) return Results.NotFound();

            return Results.Ok(DocumentenMapper.ToEnkelvoudigInformatieObject(doc, selfUrl));
        });

        app.MapGet("/documenten/api/v1/enkelvoudiginformatieobjecten/{id}/download", async (
            string id,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var doc = await rxClient.GetZaakDocumentAsync(id, ct);
            var downloadUrl = doc is not null ? DocumentenMapper.GetDownloadUrl(doc) : null;

        if (string.IsNullOrEmpty(downloadUrl))
                return Results.NotFound();

            var (stream, contentType, resolvedName) = await rxClient.DownloadFromRelativeUrlAsync(downloadUrl, ct);
            return Results.Stream(stream, contentType, resolvedName);
        });

        return app;
    }
}
