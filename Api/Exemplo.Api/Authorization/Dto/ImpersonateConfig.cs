using System.Security.Cryptography;

namespace Exemplo.Api.Authorization
{
    public class ImpersonateConfig
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string AuthUrl { get; set; }
        public string SaudeClientId { get; set; }
        public string SaudeSecret { get; set; }
        public string SaudeUrlRetorno { get; set; }
        public string SaudeAuthorizationEndpoint { get; set; }
        public string SaudeTokenEndpoint { get; set; }
        public string SaudeUserInformationEndpoint { get; set; }
        public string CallbackPath { get; set; }
    }

    public class ImpersonatePortalConfig
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string AuthUrl { get; set; }
    }

    public class TemporaryRsaKey
    {
        public string KeyId { get; set; }
        public RSAParameters Parameters { get; set; }
    }
}







