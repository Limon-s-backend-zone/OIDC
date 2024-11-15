using IdentityModel.Client;
using Newtonsoft.Json;

namespace Movie.UI.ApiServices;

public class MovieService : IMovieService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MovieService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Models.Movie>> GetMoviesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("MovieApiClient");
        var request = new HttpRequestMessage(HttpMethod.Get, "api/Movies");
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