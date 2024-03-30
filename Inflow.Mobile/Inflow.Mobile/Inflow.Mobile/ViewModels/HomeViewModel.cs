using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Inflow.Mobile.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private IProductDataStore _productDataStore;

        public ObservableCollection<TopFilter> TopFilters { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        private string _searchString = string.Empty;

        public string SearchString
        {
            get => _searchString;
            set
            {
                SetProperty(ref _searchString, value);
                ApplyFilters();
            }
        }
        public ProductFilters Filters
        {
            get
            {
                return new ProductFilters(_searchString, "");
            }
        }

        public HomeViewModel(IProductDataStore productDataStore)
        {
            _productDataStore = productDataStore;
            Title = "Home";

            TopFilters = new ObservableCollection<TopFilter>()
            {
                new TopFilter(1, "All"),
                new TopFilter(2, "Sales"),
                new TopFilter(3, "Popular"),
                new TopFilter(4, "Recommended"),
            };
            Products = new ObservableCollection<Product>();
        }

        public async Task LoadData()
        {
            Products.Clear();
            IsBusy = true;

            try
            {
                Products.Clear();
                var products = await _productDataStore.GetProductsAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadMoreData()
        {
            IsBusy = true;

            try
            {
                var products = await _productDataStore.GetNextPageAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ApplyFilters()
        {
            IsBusy = true;
            Products.Clear();
            try
            {
                var filteredProducts = await _productDataStore.FilterProducts(Filters);

                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
