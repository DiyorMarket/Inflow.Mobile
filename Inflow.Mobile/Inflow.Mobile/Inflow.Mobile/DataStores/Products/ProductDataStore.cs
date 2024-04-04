using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Products
{
    internal class ProductDataStore : IProductDataStore
    {
        private readonly ApiClient _api;
        private ApiResponse<Product> currentReponse;
        private ProductFilters currentFilters;
        private bool check = true;
        private bool checkForLoadData = true;

        public ProductDataStore(ApiClient api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            if (check || checkForLoadData)
            {
                check = false;
                checkForLoadData = false;
            currentReponse = await _api.GetAsync<Product>("products");
            }
            check = true;
            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> GetNextPageAsync(ProductFilters filters)
        {
            var next = currentReponse.Links
                .FirstOrDefault(x => x.Rel.Equals("next", StringComparison.InvariantCultureIgnoreCase));

            string queryParams = GetQueryParams(filters);
            string resource = $"products?{queryParams}";
            if (next is null)
            {
                Enumerable.Empty<Product>();
            }

            if (check)
            {
                check = false;

                currentReponse = await _api.GetAsync<Product>(next.Href + resource);
            }
            check = true;
            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> FilterProducts(ProductFilters filters)
        {
            string queryParams = GetQueryParams(filters);
            string resource = string.IsNullOrEmpty(queryParams)
                ? $"products?pageNumber=${currentReponse.Metadata.PageNumber}"
                : $"products?{queryParams}";
            if (check)
            {
                check = false;
            currentReponse = await _api.GetAsync<Product>(resource);
            }
            check = true;
            return currentReponse.Data;
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        private static string GetQueryParams(ProductFilters filters)
        {
            StringBuilder queryParams = new StringBuilder();

            if (filters.CategoryId != 0)
            {
                queryParams.Append($"categoryId={filters.CategoryId}&");
            }

            if (filters.SearchString != null)
            {
                queryParams.Append($"SearchString={filters.SearchString}&");
            }

            if (filters.LowestPrice != 0)
            {
                queryParams.Append($"PriceLessThan={filters.HighestPrice / (decimal)1.5}&");
            }

            if (filters.HighestPrice != 0)
            {
                queryParams.Append($"PriceGreaterThan={filters.LowestPrice / (decimal)1.5}");
            }

            return queryParams.ToString().TrimEnd('&');
        }
    }
}
