using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer.Configs;

public class Config
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "movieClient",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = { "movieApi" }
            },
            new Client()
            {
                ClientId = "moviesUIClient",
                ClientName = "Movie MVC Web Client",
                AllowedGrantTypes = GrantTypes.Code,
                AllowRememberConsent = false,
                RedirectUris = new List<string>()
                {
                    "https://localhost:5010/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>()
                {
                    "https://localhost:5010/signout-callback-oidc"
                },
                ClientSecrets = new List<Secret>()
                {
                    new Secret("Secret".Sha256())
                },
                AllowedScopes = new List<string>()
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("movieApi", "Movie API")
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
           
        };
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
           new IdentityResources.OpenId(),
           new IdentityResources.Profile()
        };
    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
           new TestUser()
           {
               SubjectId = "0042746f-d8fe-4d74-93ce-d6bcc5c55215",
               Username = "limon",
               Password = "limon",
               Claims = new List<Claim>()
               {
                   new Claim(JwtClaimTypes.GivenName,"limon"),
                   new Claim(JwtClaimTypes.FamilyName,"Malek")
               }
           }
        };
}