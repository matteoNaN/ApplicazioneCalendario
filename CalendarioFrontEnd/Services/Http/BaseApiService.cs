using SharedLibrary.Helpers.ApiResponse;
using System.Net.Http.Json;

namespace CalendarioFrontEnd.Services.Http
{
    public class BaseApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public BaseApiService(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }

        public async Task<Result<List<T>>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<Result<List<T>>>(_endpoint);
        }

        public async Task<Result<T>> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Result<T>>($"{_endpoint}/{id}");
        }

        public async Task<Result<T>> CreateAsync(T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }

        public async Task<Result<T>> UpdateAsync(int id, T entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}/{id}", entity);
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }

        public async Task<Result<T>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}/{id}");
            return await response.Content.ReadFromJsonAsync<Result<T>>();
        }
    }
}
