using System.Net;
using RxEnterprise.Client;
using RxEnterpriseZgwProxy.Shared;

namespace RxEnterpriseZgwProxy.Catalogi;

public static class CatalogiEndpoints
{
    public static IEndpointRouteBuilder MapCatalogiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/catalogi/api/v1/zaaktypen/{id}", async (
            string id,
            IRxEnterpriseClient rxClient,
            CancellationToken ct) =>
        {
            var sleutel = Base64Encoder.Decode(id);

            RxZaaktype? zaaktype = null;
            try
            {
                zaaktype = await rxClient.GetZaaktypeAsync(sleutel, ct);
            }
            // a zaak can refer to a zaaktype that doesn't exist in Rx.Enterprise;
            // KISS fails the whole zaken list if one zaaktype can't be fetched
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
            }

            return Results.Ok(new ZgwZaaktype
            {
                Id = zaaktype?.Sleutel ?? sleutel,
                Omschrijving = zaaktype is null ? sleutel : zaaktype.Onderwerp ?? string.Empty,
            });
        });

        app.MapGet("/catalogi/api/v1/statustypen/{id}", (string id, HttpRequest request) =>
        {
            var name = Base64Encoder.Decode(id);
            var selfUrl = $"{request.Scheme}://{request.Host}/catalogi/api/v1/statustypen/{id}";
            return Results.Ok(new ZgwStatustype { Url = selfUrl, Omschrijving = name });
        });

        return app;
    }
}
