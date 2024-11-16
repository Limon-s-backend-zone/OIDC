using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Movie.UI.ApiServices;
using Movie.UI.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.AccessDeniedPath = "/Home/AccessDenied";
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = "https://localhost:5000";
        opt.ClientId = "moviesUIClient";
        opt.ClientSecret = "Secret";
        opt.ResponseType = "code id_token"; //hybrid flow
        opt.Scope.Add("openid");
        opt.Scope.Add("profile");
        opt.Scope.Add("address");
        opt.Scope.Add("email");
        
        opt.Scope.Add("roles");
        opt.ClaimActions.MapUniqueJsonKey("role", "role");
        
        opt.Scope.Add("movieApi");
        opt.SaveTokens = true;
        opt.GetClaimsFromUserInfoEndpoint = true;

        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role
        };
    });

builder.Services.AddTransient<AuthenticationDelegatingHandler>();
// making base client request
builder.Services.AddHttpClient("MovieApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7000/");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

// identity provider
builder.Services.AddHttpClient("IDPClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5000/");
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});
builder.Services.AddHttpContextAccessor();
// accessing for token endpoint
// builder.Services.AddSingleton(new ClientCredentialsTokenRequest
// {
//     Address = "https://localhost:5000/connect/token",
//     ClientId = "movieClient",
//     ClientSecret = "secret",
//     Scope = "movieApi"
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();