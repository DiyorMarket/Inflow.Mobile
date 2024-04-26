using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Sales
{
    public class SaleDataStore : ISaleDataStore
    {
        private readonly ApiClient _client;
        private ApiResponse<Sale> _response;
        public SaleDataStore(ApiClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<Sale>> GetSales(int CustomerId)
        {
            _response = await _client.GetAsync<Sale>($"Sales?CustomerId={CustomerId}&orderBy=expiredatedesc");

            return _response.Data;
        }

        public async Task<Sale> CreateSale(Sale sale)
        {
            var json = JsonConvert.SerializeObject(sale);
            var response = await _client.PostAsync<Sale>("Sales", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating sales.");
            }

            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Sale>(jsonResponse);
        }
    }
}
