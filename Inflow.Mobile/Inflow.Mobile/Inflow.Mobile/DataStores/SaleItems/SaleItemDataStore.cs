using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.SaleItems
{
    public class SaleItemDataStore : ISaleItemDataStore
    {
        private readonly ApiClient _client;
        private ApiResponse<SaleItem> _response;
        public SaleItemDataStore(ApiClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<SaleItem>> GetSaleItems(int SaleId)
        {
            _response = await _client.GetAsync<SaleItem>($"SaleItems?SaleId={SaleId}");

            return _response.Data;
        }
    }
}