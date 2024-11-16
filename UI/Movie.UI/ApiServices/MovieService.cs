using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movie.UI.Models;
using Newtonsoft.Json;

namespace Movie.UI.ApiServices;

public class MovieService : IMovieService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MovieService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserInfoViewModel> GetUserInfoAsync()
    {
        var idpClient = _httpClientFactory.CreateClient("IDPClient");
        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();
        if (metaDataResponse.IsError) throw new HttpRequestException("Something went wrong in the identity server");
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        var userInfoResponse = await idpClient.GetUserInfoAsync(new UserInfoRequest()
        {
            Address = metaDataResponse.UserInfoEndpoint,
            Token = accessToken
        });
        if (userInfoResponse.IsError) throw new HttpRequestException("Something went wrong in getting userinfo");
        var userInfo = new Dictionary<string, string>();
        foreach (var claim in userInfoResponse.Claims)
        {
            userInfo.Add(claim.Type, claim.Value);
        }

        return new UserInfoViewModel(userInfo);
    }
    public async Task<IEnumerable<Models.Movie>> GetMoviesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("MovieApiClient");
        var request = new HttpRequestMessage(HttpMethod.Get, "Movies");
        var httResponse = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        var contents = await httResponse.Content.ReadAsStringAsync();
        var movies = JsonConvert.DeserializeObject<List<Models.Movie>>(contents);
        return movies;

        // get token from identity server
        // configuration
        var apiClientCredentials = new ClientCredentialsTokenRequest
        {
            Address = "https://localhost:5000/connect/token",
            ClientId = "movieClient",
            ClientSecret = "secret",
            Scope = "movieApi"
        };
        var client = new HttpClient();
        var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:5000");
        if (discoveryDocument.IsError) return null;
        //request
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
        if (tokenResponse.IsError) return null;

        var apiClient = new HttpClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);
        var response = await apiClient.GetAsync("https://localhost:5005/api/movies");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var movieList = JsonConvert.DeserializeObject<List<Models.Movie>>(content);
        return movieList;
    }


    public async Task<Models.Movie> GetMovieByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Movie> CreateMovieAsync(Models.Movie movie)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Movie> UpdateMovieAsync(Models.Movie movie)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteMovieAsync(int id)
    {
        throw new NotImplementedException();
    }
}