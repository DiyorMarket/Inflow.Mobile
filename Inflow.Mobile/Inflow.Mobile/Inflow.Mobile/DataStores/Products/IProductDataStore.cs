using Inflow.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Products
{
    public interface IProductDataStore
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetNextPageAsync(ProductFilters filters);
        Task<IEnumerable<Product>> FilterProducts(ProductFilters filters);
        Task<Product> GetProduct(int id);
    }
}
