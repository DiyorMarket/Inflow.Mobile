using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    internal class HomeViewModel : BaseViewModel
    {
        private IProductDataStore _productDataStore;

        public ObservableCollection<TopFilter> TopFilters { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> ProductsInCart { get; set; }
        public ObservableCollection<Product> SavedProducts { get; set; }

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

        public ICommand AddToCartCommand { get; }
        public ICommand AddToSavedCommand { get; }

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
            ProductsInCart = new ObservableCollection<Product>();
            SavedProducts = new ObservableCollection<Product>();

            AddToCartCommand = new Command<Product>(OnAddToCart);
            AddToSavedCommand = new Command<Product>(OnAddToSaved);
        }

        private void OnAddToCart(Product product)
        {
            var dataService = new DataService();
            var productsInCart = dataService.GetProducts("ProductsInCart");

            var existingProduct = ProductsInCart.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                ProductsInCart.Remove(existingProduct);
                existingProduct.IsInCart = false;
            }
            else
            {
                ProductsInCart.Add(product);
                product.IsInCart = true;
            }
            dataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
        }

        private void OnAddToSaved(Product product)
        {
            if (SavedProducts.Contains(product))
            {
                SavedProducts.Remove(product);
                product.IsSaved = false;
            }
            else
            {
                SavedProducts.Add(product);
                product.IsSaved = true;
            }
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
