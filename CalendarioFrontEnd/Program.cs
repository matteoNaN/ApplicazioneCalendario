using CalendarioFrontEnd.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using MudBlazor.Services;
using CalendarioFrontEnd.Services.Http;
using SharedLibrary.Helpers.Api;
using MudBlazor;

namespace CalendarioFrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



            builder.Services.AddHttpClient("PublicAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7163/api/");
            });

            // Registra il CustomAuthenticationStateProvider come provider di stato di autenticazione
            builder.Services.AddSingleton<JwtAuthenticationStateProvider>();
            builder.Services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

            var apiBaseUri = new Uri("https://localhost:7163");

            builder.Services.AddScoped(provider => new JwtTokenMessageHandler(apiBaseUri, provider.GetRequiredService<JwtAuthenticationStateProvider>()));
            builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = apiBaseUri)
                .AddHttpMessageHandler<JwtTokenMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

            #region//////////////////////////////// SERVIZI PER I DATI ////////////////
            builder.Services.AddScoped<GruppiHttpClass>();
            #endregion


            builder.Services.AddAuthorizationCore();

           

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            });


            var application = builder.Build();
            await RefreshToken.RefreshJwtToken(application, "https://localhost:7163/api/");

            await application.RunAsync();
        }
    }
}
