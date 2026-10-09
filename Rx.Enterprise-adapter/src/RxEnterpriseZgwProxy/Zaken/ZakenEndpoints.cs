using System.Net;
using RxEnterprise.Client;
using RxEnterpriseZgwProxy.Shared;

namespace RxEnterpriseZgwProxy.Zaken;

public static class ZakenEndpoints
{
    private const string LoggerCategory = "RxEnterpriseZgwProxy.Zaken";

    public static IEndpointRouteBuilder MapZakenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/zaken/api/v1/zaken", async (
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";

            var query = request.Query["identificatie"].FirstOrDefault() ?? string.Empty;
            if (string.IsNullOrEmpty(query))
            {
                var bsn = request.Query["rol__betrokkeneIdentificatie__natuurlijkPersoon__inpBsn"].FirstOrDefault() ?? string.Empty;
                if (!IsValidBsn(bsn))
                    return Results.Ok(ZakenMapper.ToPaginatedResult(Array.Empty<ZgwZaak>()));

                var betrokkenen = await rxClient.SearchZaakBetrokkenenByBsnAsync(bsn, ct);

                // a person can be linked to the same zaak more than once, e.g. with different roles
                var zakenForBsn = betrokkenen
                    .Where(b => !string.IsNullOrEmpty(b.Bronsleutel))
                    .DistinctBy(b => b.Bronsleutel)
                    .Select(b => ZakenMapper.ToZgwZaak(b, $"{baseUrl}/zaken/api/v1/zaken/{b.Bronsleutel}", baseUrl))
                    .ToArray();

                return Results.Ok(ZakenMapper.ToPaginatedResult(zakenForBsn));
            }
            var zaken = await rxClient.SearchZaakAsync(query, ct);

            var zaakObjects = zaken.Select(zaak =>
            {
                var selfUrl = $"{baseUrl}/zaken/api/v1/zaken/{zaak.Sleutel}";
                return ZakenMapper.ToZgwZaak(zaak, selfUrl, baseUrl);
            }).ToArray();

            return Results.Ok(ZakenMapper.ToPaginatedResult(zaakObjects));
        });

        // Get single zaak by Rx.Enterprise ID
        app.MapGet("/zaken/api/v1/zaken/{id}", async (
            string id,
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            ILoggerFactory loggerFactory,
            CancellationToken ct) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var selfUrl = $"{baseUrl}/zaken/api/v1/zaken/{id}";
            RxZaak zaak;
            try
            {
                zaak = await rxClient.GetZaakAsync(id, ct);
            }
            // a zaak found via zaak-betrokkene is not always readable via data/zaak
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                loggerFactory.CreateLogger(LoggerCategory).LogWarning("Rx.Enterprise returned 404 for zaak {ZaakId}", id);
                return Results.NotFound();
            }

            return Results.Ok(ZakenMapper.ToZgwZaak(zaak, selfUrl, baseUrl));
        });

        app.MapGet("/zaken/api/v1/rollen", async (
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            ILoggerFactory loggerFactory,
            CancellationToken ct) =>
        {
            var zaakUrl = request.Query["zaak"].FirstOrDefault() ?? string.Empty;
            var zaakId = zaakUrl.TrimEnd('/').Split('/').LastOrDefault() ?? string.Empty;

            if (string.IsNullOrEmpty(zaakId))
                return Results.Ok(ZakenMapper.ToPaginatedResult(Array.Empty<ZgwRol>()));
            
            RxZaak zaak;
            try
            {
                zaak = await rxClient.GetZaakAsync(zaakId, ct);
            }
            // a zaak found via zaak-betrokkene is not always readable via data/zaak;
            // KISS fails the whole zaken list if the rollen of one zaak can't be fetched
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                loggerFactory.CreateLogger(LoggerCategory).LogWarning("Rx.Enterprise returned 404 for zaak {ZaakId} for /rollen", zaakId);
                return Results.Ok(ZakenMapper.ToPaginatedResult(Array.Empty<ZgwRol>()));
            }

            return Results.Ok(ZakenMapper.ToPaginatedResult(ZakenMapper.ToZgwRollen(zaak)));
        });

        app.MapGet("/zaken/api/v1/statussen", () =>
            Results.Ok(ZakenMapper.ToPaginatedResult(Array.Empty<ZgwStatus>())));

        app.MapGet("/zaken/api/v1/statussen/{id}", (string id, HttpRequest request) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var selfUrl = $"{baseUrl}/zaken/api/v1/statussen/{id}";
            return Results.Ok(ZakenMapper.ToZgwStatus(Base64Encoder.Decode(id), selfUrl, baseUrl));
        });

        app.MapGet("/api/zaken/statussen/{id}", (string id, HttpRequest request) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var selfUrl = $"{baseUrl}/api/zaken/statussen/{id}";
            return Results.Ok(ZakenMapper.ToZgwStatus(Base64Encoder.Decode(id), selfUrl, baseUrl));
        });

        app.MapGet("/zaken/api/v1/zaakinformatieobjecten", async (
            HttpRequest request,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var zaakUrl = request.Query["zaak"].FirstOrDefault() ?? string.Empty;
            var zaakId = zaakUrl.TrimEnd('/').Split('/').LastOrDefault() ?? string.Empty;

            if (string.IsNullOrEmpty(zaakId))
                return Results.Ok(Array.Empty<ZgwZaakInformatieObject>());

            var docs = await rxClient.SearchZaakDocumentsAsync(zaakId, ct);
            return Results.Ok(ZakenMapper.ToZaakInformatieObjecten(docs, zaakUrl, baseUrl));
        });
        
        return app;
    }

    // the BSN ends up in the Rx.Enterprise search, so only allow exactly 9 digits
    private static bool IsValidBsn(string bsn) =>
        bsn.Length == 9 && bsn.All(char.IsAsciiDigit);
}
