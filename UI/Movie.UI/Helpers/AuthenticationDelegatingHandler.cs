using IdentityModel.Client;

namespace Movie.UI.Helpers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly ClientCredentialsTokenRequest _clientCredentialsTokenRequest;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthenticationDelegatingHandler(IHttpClientFactory httpClientFactory,
        ClientCredentialsTokenRequest clientCredentialsTokenRequest)
    {
        _httpClientFactory = httpClientFactory;
        _clientCredentialsTokenRequest = clientCredentialsTokenRequest;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient("IDPClient");
        var tokenResponse =
            await httpClient.RequestClientCredentialsTokenAsync(_clientCredentialsTokenRequest, cancellationToken);
        if (tokenResponse.IsError) throw new HttpRequestException($"{tokenResponse.ErrorDescription}");
        request.SetBearerToken(tokenResponse.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}