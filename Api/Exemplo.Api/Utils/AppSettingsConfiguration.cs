using Exemplo.Api.Authorization;

namespace Exemplo.Api.Utils;

public static class AppSettingsConfiguration
{
    private static ImpersonateConfig _impersonate;
    private static UrlConfig _url;

    public static ImpersonateConfig GetImpersonate()
    {
        return _impersonate;
    }

    internal static void Set(ImpersonateConfig impersonate)
    {
        _impersonate = impersonate;
    }

    public static UrlConfig GetURL()
    {
        return _url;
    }

    internal static void Set(UrlConfig url)
    {
        _url = url;
    }
}






