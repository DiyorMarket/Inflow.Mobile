using Inflow.Mobile.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inflow.Mobile.Services
{
    public class ApiClient
    {
        private const string BaseUrl = "https://xsx0lcnz-7258.asse.devtunnels.ms/api";
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string resource)
        {
            if (resource.Contains("https"))
            {
                var index = resource.IndexOf("api/");
                resource = resource.Substring(index+4); 
            }
            try
            {
                HttpResponseMessage response = await _client.GetAsync(BaseUrl + "/" + resource);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get data from {resource}. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse<T>>(json) 
                       ?? throw new JsonSerializationException();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
