using SharedLibrary.DTOs;
using SharedLibrary.Helpers.Api;
using SharedLibrary.Helpers.ApiResponse;
using SharedLibrary.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CalendarioFrontEnd.Services.Http
{
    public class GruppiHttpClass
    {
        private IHttpClientFactory _httpClientFactory;


        public GruppiHttpClass(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
            

        public async Task<Result> AggiungiGruppo(CreazioneGruppoDTO gruppoToAdd)
        {
            
            var request = new HttpRequestMessage(HttpMethod.Post,$"/api/{ControllerConstants.GruppiController}AggiungiGruppo");

            var content = new StringContent(JsonSerializer.Serialize(gruppoToAdd), Encoding.UTF8,"application/json");

            request.Content = content;

            var client = _httpClientFactory.CreateClient("ApiClient");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            var errorMessage = await response.Content.ReadAsStringAsync();

            return Result.Failure(errorMessage);


        }

    }
}
