namespace RxEnterpriseZgwProxy.Configuration;

public static class ConfigurationExtensions
{
    public static string Require(this IConfiguration config, string key) =>
        config[key] is { Length: > 0 } value
            ? value
            : throw new InvalidOperationException($"'{key}' is required. Set it in .env or as an environment variable.");
}
