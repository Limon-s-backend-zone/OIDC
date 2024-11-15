using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movie.UI.Helpers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    // private readonly ClientCredentialsTokenRequest _clientCredentialsTokenRequest;
    private readonly IHttpContextAccessor _httpContextAccessor;
    // private readonly IHttpClientFactory _httpClientFactory;

    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        // _httpClientFactory = httpClientFactory;
        // _clientCredentialsTokenRequest = clientCredentialsTokenRequest;
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <summary>
    ///     Getting token from httpContext
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (!string.IsNullOrWhiteSpace(accessToken))
            request.SetBearerToken(accessToken);
        return await base.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// // getting token from calling identity server
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    // protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    //     CancellationToken cancellationToken)
    // {
    //     var httpClient = _httpClientFactory.CreateClient("IDPClient");
    //     var tokenResponse =
    //         await httpClient.RequestClientCredentialsTokenAsync(_clientCredentialsTokenRequest, cancellationToken);
    //     if (tokenResponse.IsError) throw new HttpRequestException($"{tokenResponse.ErrorDescription}");
    //     request.SetBearerToken(tokenResponse.AccessToken);
    //     return await base.SendAsync(request, cancellationToken);
    // }
}