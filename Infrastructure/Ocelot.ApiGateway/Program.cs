using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Services.AddOcelot(builder.Configuration);

var authenticationProviderKey = "IdentityApiKey";
builder.Services.AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, opt =>
    {
        opt.Authority = "https://localhost:5000";
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();
app.Run();
