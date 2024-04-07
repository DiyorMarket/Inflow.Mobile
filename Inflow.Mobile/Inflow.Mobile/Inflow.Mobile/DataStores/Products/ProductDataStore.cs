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
        private bool check = true;
        private bool checkForLoadData = true;

        public ProductDataStore(ApiClient api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            currentReponse = await _api.GetAsync<Product>("products");

            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> GetNextPageAsync(ProductFilters filters)
        {
            var next = currentReponse.Links
                .FirstOrDefault(x => x.Rel.Equals("next", StringComparison.InvariantCultureIgnoreCase));

            if (next is null)
            {
                return Enumerable.Empty<Product>();
            }

            currentReponse = await _api.GetAsync<Product>(next.Href, true);

            return currentReponse.Data;
        }

        public async Task<IEnumerable<Product>> FilterProducts(ProductFilters filters)
        {
            string queryParams = GetQueryParams(filters);
            string resource = string.IsNullOrEmpty(queryParams)
                ? $"products?pageNumber=${currentReponse.Metadata.PageNumber}"
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
            if (!string.IsNullOrEmpty(filters.Sort))
            {
                if (filters.Sort.Contains("asc"))
                {
                    filters.Sort = filters.Sort.TrimEnd("asc".ToCharArray());
                }
                queryParams.Append($"OrderBy={filters.Sort}");
            }

            if (filters.CategoryId != 0)
            {
                queryParams.Append($"CategoryId={filters.CategoryId}&");
            }

            if (!string.IsNullOrEmpty(filters.SearchString))
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
