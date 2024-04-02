using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
