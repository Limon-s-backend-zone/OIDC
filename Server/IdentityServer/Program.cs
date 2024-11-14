using IdentityServer.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    // .AddInMemoryIdentityResources(Config.IdentityResources)
    // .AddInMemoryApiResources(Config.ApiResources)
    // .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();

//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}


app.UseHttpsRedirection();

app.UseIdentityServer();
// app.UseAuthorization();


// app.MapControllers();

app.Run();