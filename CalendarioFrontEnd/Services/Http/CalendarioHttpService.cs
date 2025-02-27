using SharedLibrary.DTOs;
using SharedLibrary.Helpers.Api;
using SharedLibrary.Helpers.ApiResponse;
using System.Net.Http;
using System.Text;
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

        public async Task<Result> AddImpegnoCalendario(AggiungiImpegnoDTO aggiungiImpegno)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ControllerConstants.GruppiController}AddEventoCalendario");
            request.Content = new StringContent(JsonSerializer.Serialize(aggiungiImpegno), Encoding.UTF8, "application/json");
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
