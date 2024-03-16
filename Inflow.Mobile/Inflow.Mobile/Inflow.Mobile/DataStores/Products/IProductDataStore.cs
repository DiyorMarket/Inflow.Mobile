using Inflow.Mobile.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Products
{
    public interface IProductDataStore
    {
        Task<GetProductsResponse> ProductsAsync();
    }
}
