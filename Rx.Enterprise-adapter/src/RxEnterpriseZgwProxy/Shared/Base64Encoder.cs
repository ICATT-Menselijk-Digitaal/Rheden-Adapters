using System.Text;

namespace RxEnterpriseZgwProxy.Shared;

public static class Base64Encoder
{
    public static string Encode(string value) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(value))
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

    public static string Decode(string segment)
    {
        try
        {
            var padded = segment.Replace('-', '+').Replace('_', '/');
            padded = padded.PadRight(padded.Length + (4 - padded.Length % 4) % 4, '=');
            return Encoding.UTF8.GetString(Convert.FromBase64String(padded));
        }
        catch
        {
            return segment;
        }
    }
}
