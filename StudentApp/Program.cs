#region using
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Repo;
using StudentApp.Services;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using StudentApp.Configurations;
#endregion

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Staging,
    WebRootPath = "wwwroot"
});

var config = builder.Configuration.Get<AppConfig>();
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentAppContext>(option =>
    option.UseSqlServer(config.SqlServer.StudentAppContext ?? throw new InvalidOperationException("Connection string 'StudentAppContext' not found.")));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(config.AzureAd);

#region AddingServicesToTheContainer
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
#endregion

#region SecureApi
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Azure AD Demo", Version = "v1" });
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Oauth2.0 which uses AuthorizationCode flow",
            Name = "oauth2.0",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(config.SwaggerAzureAD.AuthorizationUrl),
                    TokenUrl = new Uri(config.SwaggerAzureAD.TokenUrl),
                    Scopes = new Dictionary<string, string>
                    {
                        {config.SwaggerAzureAD.Scope, "Access API as User"}
                    }
                }
            }
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference{Type=ReferenceType.SecurityScheme, Id="oauth2"}
                },
                new []{config.SwaggerAzureAD.Scope}
            }
        });
    });
#endregion

// Add interfaces (Context)
builder.Services.AddTransient<IService, Service>();
builder.Services.AddTransient<IRepo, Repo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Pipeline
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.OAuthClientId(config.SwaggerAzureAD.ClientId);
    s.OAuthUsePkce();
    s.OAuthScopeSeparator(" ");
});

app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion
