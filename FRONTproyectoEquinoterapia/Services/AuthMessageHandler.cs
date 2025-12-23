using System.Net.Http.Headers;
using Blazored.LocalStorage;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthMessageHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("token");

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        //  TOKEN EXPIRADO
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await _localStorage.RemoveItemAsync("token");

            // Forzar logout
            throw new UnauthorizedAccessException("Token expirado");
        }

        return response;
    }

}


