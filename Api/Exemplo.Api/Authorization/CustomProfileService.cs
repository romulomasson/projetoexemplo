using Exemplo.Api.Authorization.Dto;
using Exemplo.Domain.Interfaces.Application;
using Exemplo.Repository.Contexts;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Options;

namespace Exemplo.Api.Authorization;

public class CustomProfileService : IProfileService
{
    private readonly IUsuarioApplication<ExemploContext> _application;
    private readonly AuthConfig _config;

    public CustomProfileService(IUsuarioApplication<ExemploContext> application, IOptions<AuthConfig> config)
    {
        _application = application;
        _config = config.Value;
    }


    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();

        var user = _application.Get(Convert.ToInt32(sub));

        //var menus = await _application.GetMenus(user.Id);

        var claims = AuthorizationConfig.GetClaims(user, _config);

        context.IssuedClaims = claims.ToList();
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = _application.Get(Convert.ToInt32(sub));
        context.IsActive = user != null;
    }
}

