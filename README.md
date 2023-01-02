# StudentAppAzureADLogin-1.0
ASP.NET Core 6/7 MVC & EF (Code-First) (HttpGet/Post/Put/Delete) Swagger Azure AD Authentication

# How to integrate Azure AD with ASP.NET 7 - 2023 ?

********************************************************
--------------------------------------------------------
STEPS (Swagger Azure AD Autht - Swagger Azure AD Login)=
--------------------------------------------------------
********************************************************

Opening programs                     =
--------------------------------------
Open Visual Studio/-Code
After that Open your project/api (Exp.=StudentApp)


Creating Swagger Azure AD Api        =
--------------------------------------
Now we are in project/api,

- On Connected services/Solution Explorer click right & add Microsoft Identity Platform,
- On the new opened page if you are not login to Azure login/sign in to Azure,
- On the new opened page after logging in you will see your created App's name as a list,
- We are still on this page, on the upper right click (+) to create/register application,
- After that do not change anything until finish (Generated Code, Nuget Packages, Update Azure AD) 
  and click next, next (Dependency configuration progress) and close/finish.


Making changes on project/StudentApp =
--------------------------------------
- We will see on Program.cs that "builder.Services.AddAuthentication( ..........)....("AzureAd"));" is created.
- And also on Program.cs "app.UseHttpsRedirection();  app.UseAuthentication();  app.UseAuthorization();" is created too, 
  if not add them!!!.
- We will see on appsettings.json that "AzureAd";{ "Instance":..., "Domain":..., "TenantId":..., 
  "ClientId":..., "Scopes":..., etc..} is created.
- Under Packages/Dependencies, new packages are installed.


Making changes on Controllers/StudentApp =
------------------------------------------
- Open controller(exp.WeatherForecastController), under namespace if not created add these attributes:
  [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
  [Authorize]
  [ApiController]
  [Route("[controller]")]


Making changes on project using data from Azure Portal =
--------------------------------------------------------
- Open Azure Portal (use Directory in which you created SwaggerAzureADApi or signed id while creating SwaggerAzureADApi),
- On the portal, open Azure Active Directory,
- open App registrations under "Manage",
- click SwaggerAzureADApi under "Owned Applications", check data on the right side under "Essentials", 
- this data which are "Application (client) id, Directory (tenant) id etc..." is created automatically 
  when creating SwaggerAzureADApi and uploaded to appsettings.json.


RUN project whether it is working or not =
------------------------------------------
- run get method and execute,
- if you get response as "Status Code 401", it is okay and we need to make some changes in Program.cs.


Making changes on Program.cs/StudentApp =
-----------------------------------------
- go to "builder.Services.AddSwaggerGen();" and add these codes to add OpenApiInfo & OpenApiSecurityScheme:

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

- also add these codes to "app.UseSwaggerUI();" :

  app.UseSwaggerUI(s =>
  {
    s.OAuthClientId(builder.Configuration["SwaggerAzureAd:ClientId"]);
    s.OAuthUsePkce();
    s.OAuthScopeSeparator(" ");
  });


Making changes on Azure Portal =
--------------------------------
- Open Azure Portal (use Directory in which you created SwaggerAzureADApi or signed id while creating SwaggerAzureADApi),
- On the portal, open Azure Active Directory,
- open App registrations under "Manage",
- click (+ New registration) on the upper right side and 
- give name to registration (exp."SwaggerClientAppRegistration") and register with default values.


Making changes on project/StudentApp =
--------------------------------------
- go to appsettings.json and add these codes from program.cs following "AzureAd" :

    "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "11august22.onmicrosoft.com",
    "TenantId": "d5af55b1-09d1-4c62-b91b-d108fd981704",
    "ClientId": "a3f042ab-660c-4b16-92ee-6c1ea013f75e",
    "CallbackPath": "/signin-oidc",
    "Scopes": "access_as_user"
  },
  "SwaggerAzureAD": {
    "AuthorizationUrl": "https://login.microsoftonline.com/d5af55b1-09d1-4c62-b91b-d108fd981704/oauth2/v2.0/authorize",  
              >>>> take this data from "SwaggerClientAppRegistration | Endpoints" (authorization endpoint(v2))

    "TokenUrl": "https://login.microsoftonline.com/d5af55b1-09d1-4c62-b91b-d108fd981704/oauth2/v2.0/token",
              >>>> take this data from "SwaggerClientAppRegistration | Endpoints" (token endpoint(v2))

    "Scope": "api://a3f042ab-660c-4b16-92ee-6c1ea013f75e/access_as_user",
              >>>> go to "SwaggerClientAppRegistration | API permissions",
              >>>> click  (+ Add a Permission) on the new page click "My APIs",
              >>>> click project api name (exp. SwaggerAzureADApi) on the new page click "access_as_user",
              >>>> on the same page click (Add permissions)
              >>>> we are again on the "SwaggerClientAppRegistration | API permissions" page,
              >>>> click "access_as_user" and on the new page copy data "api://......................."
              >>>> paste this data to "Scope".
              

    "ClientId": "e6e468b6-e2bd-405f-8808-40aef44b49da"                  
              >>>> take this data from "SwaggerClientAppRegistration | Overview" section (Application (client) ID)

    }


Making changes on Azure Portal =
--------------------------------
- Open Azure Portal (use Directory in which you created SwaggerAzureADApi or signed id while creating SwaggerAzureADApi),
- On the portal, open Azure Active Directory,
- open App registrations under "Manage",
- click "SwaggerAzureADApi" under "Owned Applications", 
- on the new page, click "Expose an API",
- on the "Expose an API" page, if there's not any scopes, add a scope (consent to "Admins and users", 
  and give scope name (exp. "access_as_user") and save it),
- on the "Expose an API" page, click (+ Add a client application),
- write the Client Id (from "SwaggerClientAppRegistration | Overview" section (Application (client) ID)),
- after writing click "authorized scopes" also, and click "Add Application".

- open App registrations under "Manage" again,
- click "SwaggerClientAppRegistration", and click "Authentication" under "Manage",
- click (+ Add a platform)
- on the new page click ("Single-page application"),
- on the new opened page click ("Access tokens" and "ID tokens") but don't save because we need "Redirect URIs",
- so NOW, RUN program from Visual Studio/-code, !!!
  and on the opened page copy the url (exp."https://localhost:7019/swagger")
- and paste it to "Redirect URIs" as (exp."https://localhost:7019/swagger/oauth2-redirect.html"),
- now you can click save/configure "Configure single-page application". Dont close RUNNING program. !!!


GO BACK TO Running program, Swagger page=
----------------------------------------
- Click ("Authorize") button,
- on the new page click data ("api://...................") under "Scopes" and then click "Authorize" button.
- on the new opened page Sign in with your account (account logging information in which you created SwaggerAzureADApi)
- on the new page give permission and click "Accept",
- now WE GOT TOKEN.
- TURN TO running program, swagger page,
- RUN Methods (get/post/put/update/delete)
- NOW WE HAVE ACCESS TO DATA, GETTING RESPONSE, AND METHODS WORKING  !!!!!  :))

*******************************************************************************
