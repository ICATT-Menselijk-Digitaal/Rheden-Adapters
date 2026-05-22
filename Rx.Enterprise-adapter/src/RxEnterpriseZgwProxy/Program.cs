using DotNetEnv;
using RxEnterprise.Client;
using RxEnterpriseZgwProxy.Auth;
using RxEnterpriseZgwProxy.Catalogi;
using RxEnterpriseZgwProxy.Configuration;
using RxEnterpriseZgwProxy.Documenten;
using RxEnterpriseZgwProxy.Zaken;

var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (!File.Exists(envPath))
    envPath = Path.Combine(AppContext.BaseDirectory, ".env");
Env.Load(envPath);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddZgwAuth(builder.Configuration);
builder.Services.AddRxEnterpriseClient(o =>
{
    o.BaseUrl = builder.Configuration.Require("RxEnterprise:BaseUrl");
    o.CertificatePath = builder.Configuration.Require("RxEnterprise:CertificatePath");
    o.PrivateKeyPath = builder.Configuration.Require("RxEnterprise:PrivateKeyPath");
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz").AllowAnonymous();
app.MapZakenEndpoints();
app.MapCatalogiEndpoints();
app.MapDocumentenEndpoints();

app.Run();
