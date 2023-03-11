#region using
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Repo;
using StudentApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<StudentAppContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("StudentAppContext") ?? throw new InvalidOperationException("Connection string 'StudentAppContext' not found.")));
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

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
    c=>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title ="Swagger Azure AD Demo", Version= "v1" });
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Oauth2.0 which uses AuthorizationCode flow",
            Name = "oauth2.0",
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAD:AuthorizationUrl"]),
                    TokenUrl = new Uri(builder.Configuration["SwaggerAzureAD:TokenUrl"]),
                    Scopes = new Dictionary<string, string>
                    {
                        {builder.Configuration["SwaggerAzureAd:Scope"], "Access API as User"}
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
                new []{builder.Configuration["SwaggerAzureAd:Scope"]}
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
    s.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
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
