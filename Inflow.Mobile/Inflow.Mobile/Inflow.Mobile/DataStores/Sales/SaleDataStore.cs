using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
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
            _response = await _client.GetAsync<Sale>($"Sales?CustomerId={CustomerId}");

            return _response.Data;
        }
    }
}
