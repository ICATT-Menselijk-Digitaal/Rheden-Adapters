namespace RxEnterprise.Client;

public sealed class RxEnterpriseClientOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string CertificatePath { get; set; } = string.Empty;
    public string PrivateKeyPath { get; set; } = string.Empty;
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}
