﻿using IdentityServer4.Models;
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
           
        };
    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
           
        };
}