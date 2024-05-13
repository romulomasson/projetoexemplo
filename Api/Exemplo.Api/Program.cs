using Exemplo.Api.Authorization;
using Exemplo.Api.Authorization.Dto;
using Exemplo.Api.Filters;
using Exemplo.Api.Helpers;
using Exemplo.Domain.Interfaces;
using Exemplo.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Logging;
using System.Security.Authentication;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IdentityModelEventSource.ShowPII = true;

builder.Host.ConfigureServices((context, services) =>
{
    Ioc.Initialize(services, context.Configuration);
    services.Configure<AuthConfig>(configuration.GetSection(nameof(AuthConfig)));
    services.AddScoped(typeof(IUserHelper), typeof(UsuarioHelper));
});

var authConfig = new AuthConfig();
configuration.GetSection(nameof(AuthConfig)).Bind(authConfig);

var urlConfig = new UrlConfig();
configuration.GetSection(nameof(UrlConfig)).Bind(urlConfig);

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(DomainExceptionFilter));
    options.ModelBinderProviders.Insert(0, new ProviderModelBinder());
    options.EnableEndpointRouting = false;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //DI Automapper ServiceLocator foi Descontinuado
/* #################### */
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader()
                                                  .AllowAnyMethod(); 
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RsaKeyService rsa = new RsaKeyService(builder.Environment, TimeSpan.FromDays(30));
builder.Services.AddSingleton(provider => rsa);

builder.Services.AddIdentityServer()
       .AddSigningCredential(rsa.GetKey(), RsaSigningAlgorithm.RS512)
       .AddInMemoryClients(AuthorizationConfig.GetClients(authConfig, urlConfig)) // Configure seus clientes
       .AddInMemoryApiResources(AuthorizationConfig.GetApiResources(authConfig)) // Configure suas APIs
       .AddInMemoryApiScopes(AuthorizationConfig.GetApiScopes(authConfig)) // Configure escopos
       .AddResourceOwnerValidator<GrantValidator>()
       .AddProfileService<CustomProfileService>();



//JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);




builder.Services.AddAuthentication("Bearer")
        .AddIdentityServerAuthentication(options =>
        {
            options.Authority = authConfig.AuthUrl; // URL do IdentityServer
            options.ApiName = authConfig.ClientId; // Nome da sua API
            options.RequireHttpsMetadata = false;
            options.JwtBackChannelHandler = GetHandler();
        });

HttpClientHandler GetHandler()
{
    var handler = new HttpClientHandler();
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
    handler.SslProtocols = SslProtocols.Tls12;
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    return handler;
};

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

var forwardOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    RequireHeaderSymmetry = false
};

forwardOptions.KnownNetworks.Clear();
forwardOptions.KnownProxies.Clear();

// ref: https://github.com/aspnet/Docs/issues/2384
app.UseForwardedHeaders(forwardOptions);
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseIdentityServer();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});


app.Run();


