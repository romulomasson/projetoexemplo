using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Exemplo.Api.Authorization.Dto;
using Exemplo.Domain.Entities;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Exemplo.Api.Authorization
{
    public static class AuthorizationConfig
    {
        
        public static IEnumerable<ApiResource> GetApiResources(AuthConfig _config)
        {
            return new List<ApiResource>
            {
                new ApiResource( _config.ClientId,  _config.ClientId){Scopes = {"portal", "mobile", "offline_access", "portal offline_access" },}
            };
        }

        public static IEnumerable<Client> GetClients(AuthConfig _config, UrlConfig _urlConfig) => new List<Client>
        {
            new Client
            {
                ClientId = _config.ClientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets ={ new Secret(_config.Secret.Sha256()) },
                AllowedScopes = {"portal","mobile",_config.ClientId,"offline_access","portal offline_access",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OfflineAccess},
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AccessTokenLifetime = 86400 * 5,
                IdentityTokenLifetime = 86400 * 5,
                AlwaysSendClientClaims = true,
                UpdateAccessTokenClaimsOnRefresh=true,
                AllowedCorsOrigins = {
                    _urlConfig.Site
                }
            },
        };


        public static RsaSecurityKey GetCertificate()
        {
            var filename = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.rsa");

            if (File.Exists(filename))
            {
                var keyFile = File.ReadAllText(filename);
                var tempKey = JsonConvert.DeserializeObject<TemporaryRsaKey>(keyFile, new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() });

                return CreateRsaSecurityKey(tempKey.Parameters, tempKey.KeyId);
            }
            else
            {
                var key = CreateRsaSecurityKey();
                RSAParameters parameters;

                if (key.Rsa != null)
                    parameters = key.Rsa.ExportParameters(includePrivateParameters: true);
                else
                    parameters = key.Parameters;

                var tempKey = new TemporaryRsaKey
                {
                    Parameters = parameters,
                    KeyId = key.KeyId
                };

                File.WriteAllText(filename, JsonConvert.SerializeObject(tempKey, new JsonSerializerSettings { ContractResolver = new RsaKeyContractResolver() }));
                return key;
            }
        }

        public static IEnumerable<ApiScope> GetApiScopes(AuthConfig _config)
        {
            return new[]
            {
                new ApiScope(name: "portal", displayName: "portal"),
                new ApiScope(name: _config.ClientId,displayName: _config.ClientId)
            };
        }

        public static IdentityServerAuthenticationOptions GetServerAutentication(IdentityServerAuthenticationOptions options, AuthConfig _config)
        {
            options.ApiName = _config.ClientId;
            options.Authority = _config.AuthUrl;
            options.RequireHttpsMetadata = false; // only for development
            options.ApiSecret = _config.Secret;
            options.SupportedTokens = SupportedTokens.Both;

            return options;
        }
       

        public static Claim[] GetClaims(Usuario usuario, AuthConfig _config)
        {
            var claims = new[]
            {
                new Claim(JwtClaimTypes.Id, usuario.Id.ToString()),
                new Claim(JwtClaimTypes.Role, usuario.UsuarioTipoId.ToString()),
                new Claim(JwtClaimTypes.Issuer, _config.AuthUrl),
                new Claim(JwtClaimTypes.Audience, _config.ClientId),
                new Claim(JwtClaimTypes.Email, usuario.Email),
                new Claim("name",usuario.Nome.ToString()),
                new Claim("empresaid",usuario.EmpresaId.ToString())
            };

            return claims;
        }
        private static RsaSecurityKey CreateRsaSecurityKey()
        {
            var rsa = RSA.Create();
            RsaSecurityKey key;

            if (rsa is RSACryptoServiceProvider)
            {
                rsa.Dispose();
                var cng = new RSACng(2048);

                var parameters = cng.ExportParameters(includePrivateParameters: true);
                key = new RsaSecurityKey(parameters);
            }
            else
            {
                rsa.KeySize = 2048;
                key = new RsaSecurityKey(rsa);
            }

            key.KeyId = CryptoRandom.CreateUniqueId(16);
            return key;
        }

        private static RsaSecurityKey CreateRsaSecurityKey(RSAParameters parameters, string id)
        {
            var key = new RsaSecurityKey(parameters)
            {
                KeyId = id
            };
            return key;
        }
        private class RsaKeyContractResolver : DefaultContractResolver
        {
            protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                property.Ignored = false;

                return property;
            }
        }
    }
}






