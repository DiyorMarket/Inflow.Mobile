using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private IProductDataStore _ProductDataStore;
        public ObservableCollection<TopFilter> TopFilters { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public HomeViewModel(IProductDataStore productDataStore)
        {
            _ProductDataStore = productDataStore;
            Title = "Home";
            TopFilters = new ObservableCollection<TopFilter>()
            {
                new TopFilter(1, "All"),
                new TopFilter(2, "Sales"),
                new TopFilter(3, "Popular"),
                new TopFilter(4, "Recommended"),
            };

            Products = new ObservableCollection<Product>();

            LoadData();
        }

        public async Task LoadData()
        {
            Products.Clear();
            try
            {
                Products.Clear();
                var products = await _ProductDataStore.GetProductsAsync();
                //foreach (var product in products)
                //{
                //    Products.Add(product);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }
    }
}
