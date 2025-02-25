using SharedLibrary.DTOs;
using SharedLibrary.Helpers.Api;
using SharedLibrary.Helpers.ApiResponse;
using System.Net.Http;
using System.Text.Json;

namespace CalendarioFrontEnd.Services.Http
{
    public class CalendarioHttpService
    {
        private IHttpClientFactory _httpClientFactory;

        public CalendarioHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<Result<CalendarioDTO>> GetCalendarioGruppo(Guid gruppoId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{ControllerConstants.GruppiController}GetCalendarioGruppo?gruppoId={gruppoId}");
            var client = _httpClientFactory.CreateClient("ApiClient");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var calendario = JsonSerializer.Deserialize<CalendarioDTO>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (calendario is null)
                {
                    return Result.Failure<CalendarioDTO>("errore nei dati");
                }
                return Result.Success<CalendarioDTO>(calendario);
            }
            var errorMessage = await response.Content.ReadAsStringAsync();
            return Result.Failure<CalendarioDTO>(errorMessage);
        }
    }
}
