using System;
using System.Security.Cryptography;

namespace Exemplo.Api.Authorization.Dto;

public class AuthConfig
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



