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

        // id is "{documentNummer}--{virtualId}" (e.g. D2025-01-000009--1)
        app.MapGet("/documenten/api/v1/enkelvoudiginformatieobjecten/{id}", async (
            string id,
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var parts = id.Split("--");
            if (parts.Length != 2 || !int.TryParse(parts[1], out var virtualId))
                return Results.NotFound();

            var baseUrl = $"{request.Scheme}://{request.Host}";
            var selfUrl = $"{baseUrl}/documenten/api/v1/enkelvoudiginformatieobjecten/{Uri.EscapeDataString(id)}";

            var doc = await rxClient.GetDocumentAsync(parts[0], ct);
            return Results.Ok(DocumentenMapper.ToEnkelvoudigInformatieObject(doc, virtualId, selfUrl));
        });

        // Resolves filename from document bijlageinfo then streams the binary
        app.MapGet("/documenten/api/v1/enkelvoudiginformatieobjecten/{id}/download", async (
            string id,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var parts = id.Split("--");
            if (parts.Length != 2 || !int.TryParse(parts[1], out var virtualId))
                return Results.NotFound();

            var doc = await rxClient.GetDocumentAsync(parts[0], ct);
            var filename = DocumentenMapper.FindAttachmentFilename(doc, virtualId);

            if (string.IsNullOrEmpty(filename))
                return Results.NotFound();

            var (stream, contentType, resolvedName) = await rxClient.DownloadDocumentAsync(parts[0], filename, ct);
            return Results.Stream(stream, contentType, resolvedName);
        });

        return app;
    }
}
