using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            builder.Services.AddHttpClient("SecureAPI", client =>
                    client.BaseAddress = new Uri("https://tuo-api-server"))
                    .AddHttpMessageHandler<AuthorizationMessageHandler>();

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("SecureAPI"));

            builder.Services.AddOidcAuthentication(options => {
                builder.Configuration.Bind("Local", options.ProviderOptions);
                options.ProviderOptions.ResponseType = "code";
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.DefaultScopes.Add("profile");
            });

            await builder.Build().RunAsync();
        }
    }
}
