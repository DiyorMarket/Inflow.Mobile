using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Products
{
    internal class ProductDataStore : IProductDataStore
    {
        private readonly ApiClient _api;
        private GetBaseReponse<Product> currentReponse;
        private ProductFilters currentFilters;

        public ProductDataStore(ApiClient api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            currentReponse = await _api.GetAsync<Product>("products");

            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> GetNextPageAsync()
        {
            currentReponse = await _api.GetAsync<Product>($"products?pageNumber={currentReponse.PageNumber + 1}");

            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> FilterProducts(ProductFilters filters)
        {
            string queryParams = GetQueryParams(filters);
            string resource = string.IsNullOrEmpty(queryParams)
                ? $"products?pageNumber=${currentReponse.PageNumber}"
                : $"products?{queryParams}";

            currentReponse = await _api.GetAsync<Product>(resource);

            return currentReponse.Data;
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        private static string GetQueryParams(ProductFilters filters)
        {
            StringBuilder queryParams = new StringBuilder();

            if (filters.CategoryId != null)
            {
                queryParams.Append($"categoryId={filters.CategoryId}&");
            }

            if (filters.SearchString != null)
            {
                queryParams.Append($"SearchString={filters.SearchString}&");
            }

            if (filters.LowestPrice != null)
            {
                queryParams.Append($"lowestPrice={filters.LowestPrice}&");
            }

            if (filters.HighestPrice != null)
            {
                queryParams.Append($"highestPrice={filters.HighestPrice}");
            }

            return queryParams.ToString().TrimEnd('&');
        }
    }
}
