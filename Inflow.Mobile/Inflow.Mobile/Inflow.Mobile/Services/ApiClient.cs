using Inflow.Mobile.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Inflow.Mobile.Services
{
    public class ApiClient
    {
        private const string BaseUrl = "https://vw2plztl-7258.asse.devtunnels.ms/api";
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BaseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<GetBaseReponse<T>> GetAsync<T>(string resource)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(BaseUrl + "/" + resource);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to get data from {resource}. Status code: {response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GetBaseReponse<T>>(json) 
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
