using Inflow.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.Services
{
    internal class ProductDataStore : IDataStore<Product>
    {
        private static Category _category = new Category
        {
            Id = 1,
            Name = "Category 1"
        };

        private static List<Product> Products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Name = "Product 1",
                ImageUrl = "coca_cola.png",
                Description = "Product 1 Description",
                ExpireDate = DateTime.Now.AddMonths(2),
                QuantityInStock = 5,
                Category = _category,
                SalePrice = 1500
            },
            new Product()
            {
                Id = 2,
                Name = "Product 2",
                ImageUrl = "croissant.png",
                Description = "Product 2 Description",
                ExpireDate = DateTime.Now.AddMonths(2),
                QuantityInStock = 5,
                SalePrice = 1750,
                Category = _category,
            },
            new Product()
            {
                Id = 3,
                Name = "Product 3",
                ImageUrl = "baseball.png",
                Description = "Product 3 Description",
                ExpireDate = DateTime.Now.AddMonths(2),
                QuantityInStock = 5,
                SalePrice = 2540,
                Category = _category,
            }
        };
        public Task<bool> AddItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            return Task.FromResult(Products.AsEnumerable());
        }

        public Task<bool> UpdateItemAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
