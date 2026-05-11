using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RxEnterprise.Client;

Env.Load(Path.Combine(AppContext.BaseDirectory, ".env"));

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

static string Require(IConfiguration cfg, string key) =>
    cfg[key] is { Length: > 0 } value
        ? value
        : throw new InvalidOperationException($"'{key}' is required. Set it in .env or as an environment variable.");

var services = new ServiceCollection();

services.AddLogging(b => b.AddConsole().AddConfiguration(config.GetSection("Logging")));

services.AddRxEnterpriseClient(o =>
{
    o.BaseUrl = Require(config, "RxEnterprise:BaseUrl");
    o.CertificatePath = Require(config, "RxEnterprise:CertificatePath");
    o.PrivateKeyPath = Require(config, "RxEnterprise:PrivateKeyPath");
});

await using var provider = services.BuildServiceProvider();

var logger = provider.GetRequiredService<ILogger<IRxEnterpriseClient>>();
var client = provider.GetRequiredService<IRxEnterpriseClient>();

// for testing, you can pass a zaak ID as a command-line argument, or hardcode one here:

// var zaakId = args.Length > 0 ? args[0] : "2025-000001";
// logger.LogInformation("Sending request for zaak {ZaakId}...", zaakId);

// try
// {
//     var result = await client.GetZaakAsync(zaakId);
//     logger.LogInformation("Response:\n{Response}", result);
// }
// catch (Exception ex)
// {
//     logger.LogError(ex, "HTTP request failed: {Message}", ex.Message);
// }
