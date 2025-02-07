using CalendarioFrontEnd.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

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

            //builder.Services.AddOidcAuthentication(options =>
            //{
            //    // Configure your authentication provider options here.
            //    // For more information, see https://aka.ms/blazor-standalone-auth
            //    builder.Configuration.Bind("Local", options.ProviderOptions);
            //});

            builder.Services.AddHttpClient("PublicAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7163/api/");
            });

            // Registra il CustomAuthenticationStateProvider come provider di stato di autenticazione
            builder.Services.AddSingleton<JwtAuthenticationStateProvider>();
            builder.Services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());

            var appUri = new Uri(builder.HostEnvironment.BaseAddress);

            builder.Services.AddScoped(provider => new JwtTokenMessageHandler(appUri, provider.GetRequiredService<JwtAuthenticationStateProvider>()));
            builder.Services.AddHttpClient("https://localhost:7163/api/", client => client.BaseAddress = appUri)
                .AddHttpMessageHandler<JwtTokenMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MyApp.ServerAPI"));



            builder.Services.AddAuthorizationCore();



            var application = builder.Build();
            await RefreshToken.RefreshJwtToken(application, "https://localhost:7163/api/");
        }
    }
}
