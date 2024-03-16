using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Products
{
    internal class ProductDataStore : IProductDataStore
    {
        private readonly ApiClient _api;

        public ProductDataStore(ApiClient api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<GetProductsResponse> GetProductsAsync()
        {
            var response = await _api.GetAsync("products");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not fetch products.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result =  JsonConvert.DeserializeObject<GetProductsResponse>(json);

            return result;
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
