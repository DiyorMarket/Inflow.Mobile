using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.Services
{
    public class ApiClient
    {
        private const string baseUrl = "https://localhost:7258/api";
        private readonly HttpClient _client = new HttpClient();

        public ApiClient()
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress?.AbsolutePath + "/" + url);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching url: {url}");
            }

            return response;
        }
    }
}
