using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedLibrary.DTOs.Auth;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CalendarioFrontEnd.Auth
{
    public class JwtTokenMessageHandler : DelegatingHandler
    {
        private readonly Uri _allowedBaseAddress;
        private readonly JwtAuthenticationStateProvider _loginStateService;

        public JwtTokenMessageHandler(Uri allowedBaseAddress, JwtAuthenticationStateProvider loginStateService)
        {
            this._allowedBaseAddress = allowedBaseAddress;
            this._loginStateService = loginStateService;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.SendAsync(request, cancellationToken).Result;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri;
            var isSelfApiAccess = this._allowedBaseAddress.IsBaseOf(uri);

            if (isSelfApiAccess)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this._loginStateService.Token ?? string.Empty);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }

}


public static class RefreshToken
{
    public static async Task RefreshJwtToken(WebAssemblyHost application, string apiUrl)
    {
        using var boostrapScope = application.Services.CreateScope();
        using var api = boostrapScope.ServiceProvider.GetRequiredService<HttpClient>();

        //var refreshTokenResponse = await api.PostAsync(apiUrl,null);
        var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);

        var response = await api.SendAsync(request);

        var responsecontent = await response.Content.ReadAsStringAsync();


        var RefresTokenResponse = JsonSerializer.Deserialize<LoginResponse>(responsecontent);


        if (RefresTokenResponse is not null && RefresTokenResponse.isSuccessfull)
        {
            var loginStateService = boostrapScope.ServiceProvider.GetRequiredService<JwtAuthenticationStateProvider>();
            loginStateService.Login(RefresTokenResponse.Token);
        }
    }
}
