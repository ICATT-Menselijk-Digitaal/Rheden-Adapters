using System.Security.Cryptography;
using System.Text;
using RxEnterprise.Client;
using RxEnterpriseZgwProxy.Shared;

namespace RxEnterpriseZgwProxy.Zaken;

public static class ZakenMapper
{
    public static ZgwPaginatedResult<T> ToPaginatedResult<T>(IReadOnlyList<T> items) => new()
    {
        Count = items.Count,
        Results = items,
    };

    public static ZgwZaak ToZgwZaak(RxZaak zaak, string selfUrl, string baseUrl) => new()
    {
        Url = selfUrl,
        Uuid = zaak.Sleutel ?? string.Empty,
        Identificatie = zaak.Sleutel ?? string.Empty,
        Omschrijving = zaak.Betreft ?? string.Empty,
        Bronorganisatie = string.Empty,
        Zaaktype = zaak.Zaaktypesleutel is { Length: > 0 } s
            ? $"{baseUrl}/catalogi/api/v1/zaaktypen/{Base64Encoder.Encode(s)}"
            : string.Empty,
        Registratiedatum = zaak.Boekdatum ?? string.Empty,
        Startdatum = zaak.Boekdatum ?? string.Empty,
        Status = zaak.Afhandelingsstatus is { Length: > 0 } status
            ? $"{baseUrl}/zaken/api/v1/statussen/{Base64Encoder.Encode(status)}"
            : string.Empty,
        Toelichting = string.Empty,
    };

    public static ZgwStatus ToZgwStatus(string statusName, string selfUrl, string baseUrl) => new()
    {
        Url = selfUrl,
        Uuid = Base64Encoder.Encode(statusName),
        Zaak = null,
        Statustype = $"{baseUrl}/catalogi/api/v1/statustypen/{Base64Encoder.Encode(statusName)}",
        DatumStatusGezet = null,
        Statustoelichting = statusName,
        IndicatieLaatstGezetteStatus = null,
    };

    public static ZgwZaakInformatieObject[] ToZaakInformatieObjecten(RxDocument doc, string zaakUrl, string baseUrl)
    {
        if (string.IsNullOrEmpty(doc.Documentnummer) || doc.Bijlageinfo is null)
            return [];

        return [.. doc.Bijlageinfo
            .Where(item => item.Virtualid != 0)
            .Select(item =>
            {
                var idx = item.Virtualid - 1;
                var compoundId = $"{doc.Documentnummer}--{item.Virtualid}";
                var uuid = DeriveUuid(compoundId);
                return new ZgwZaakInformatieObject
                {
                    Url = $"{baseUrl}/zaken/api/v1/zaakinformatieobjecten/{uuid}",
                    Uuid = uuid,
                    Informatieobject = $"{baseUrl}/documenten/api/v1/enkelvoudiginformatieobjecten/{Uri.EscapeDataString(compoundId)}",
                    Zaak = zaakUrl,
                    AardRelatieWeergave = string.Empty,
                    Titel = string.Empty,
                    Beschrijving = string.Empty,
                    Registratiedatum = doc.Bijlagedatumtijd?.ElementAtOrDefault(idx) ?? string.Empty,
                    Vernietigingsdatum = null,
                    Status = null,
                };
            })];
    }

    public static IReadOnlyList<ZgwRol> ToZgwRollen(RxZaak zaak)
    {
        var rollen = new List<ZgwRol>();

        if (!string.IsNullOrEmpty(zaak.Voornamenafzender) || !string.IsNullOrEmpty(zaak.Voorlettersafzender))
        {
            rollen.Add(new ZgwRol
            {
                BetrokkeneType = "natuurlijk_persoon",
                Omschrijving = "Aanvrager",
                OmschrijvingGeneriek = "initiator",
                BetrokkeneIdentificatie = new ZgwNatuurlijkPersoon
                {
                    Voornamen = zaak.Voornamenafzender ?? string.Empty,
                    VoorvoegselGeslachtsnaam = zaak.Voorvoegselafzender ?? string.Empty,
                    Voorletters = zaak.Voorlettersafzender ?? string.Empty,
                },
            });
        }

        if (zaak.Eerstebehandelaar is { Count: >= 1 })
        {
            rollen.Add(new ZgwRol
            {
                BetrokkeneType = "natuurlijk_persoon",
                Omschrijving = "Behandelaar",
                OmschrijvingGeneriek = "behandelaar",
                BetrokkeneIdentificatie = new ZgwNatuurlijkPersoon
                {
                    Geslachtsnaam = zaak.Eerstebehandelaar[0],
                },
            });
        }

        return rollen;
    }

    private static string DeriveUuid(string input) =>
        string.IsNullOrEmpty(input)
            ? Guid.Empty.ToString()
            : new Guid(MD5.HashData(Encoding.UTF8.GetBytes(input))).ToString();
}
