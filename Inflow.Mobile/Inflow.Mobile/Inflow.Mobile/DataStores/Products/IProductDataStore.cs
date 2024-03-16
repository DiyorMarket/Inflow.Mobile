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
        Task<GetProductsResponse> GetProductsAsync();
        Task<Product> GetProduct(int id);
    }
}
