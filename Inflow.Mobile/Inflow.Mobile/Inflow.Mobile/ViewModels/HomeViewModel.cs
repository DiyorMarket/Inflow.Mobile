using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using MvvmHelpers.Commands;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inflow.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IProductDataStore _productDataStore;

        public ObservableCollection<TopFilter> TopFilters { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<string> Properties { get; private set; }
        public ObservableCollection<string> OrderBy { get; private set; }
        public ObservableCollection<Product> ProductsInCart { get; set; }
        public ObservableCollection<Product> SavedProducts { get; set; }

        public ICommand AddToCartCommand { get; }
        public ICommand AddToSavedCommand { get; }

        private string _searchString = string.Empty;
        public string SearchString
        {
            get => _searchString;
            set
            {
                SetProperty(ref _searchString, value);
                OnApplyFilters();
            }
        }

        private decimal _lowestPrice;
        public decimal LowestPrice
        {
            get => _lowestPrice;
            set => SetProperty(ref _lowestPrice, value);
        }

        private decimal _highestPrice;
        public decimal HighestPrice
        {
            get => _highestPrice;
            set => SetProperty(ref _highestPrice, value);
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        private string _selectedProperty = "Name";
        public string SelectedProperty
        {
            get => _selectedProperty;
            set => SetProperty(ref _selectedProperty, value);
        }
        private string _selectedOrderby = "Asc";
        public string SelectedOrderby
        {
            get => _selectedOrderby;
            set => SetProperty(ref _selectedOrderby, value);
        }

        public ProductFilters Filters
        {
            get
            {
                return new ProductFilters(_searchString, _selectedProperty + SelectedOrderby.ToLower(), _lowestPrice, _highestPrice,
                    SelectedCategory != null ? SelectedCategory.Id : 0);
            }
        }

        public ICommand LoadMoreCommand { get; }

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
            Categories = new ObservableCollection<Category>();
            Properties = new ObservableCollection<string>
            {
                "Id",
                "Name",
                "Price",
                "Description"
            };
            OrderBy = new ObservableCollection<string>
            {
                "Asc",
                "Desc"
            };

            LoadMoreCommand = new AsyncCommand(OnLoadMore);
            ProductsInCart = new ObservableCollection<Product>();
            SavedProducts = new ObservableCollection<Product>();
            AddToCartCommand = new MvvmHelpers.Commands.Command<Product>(OnAddToCart);
            AddToSavedCommand = new MvvmHelpers.Commands.Command<Product>(OnAddToSaved);

        }

        public async Task LoadData()
        {
            if (IsBusy)
            {
                return;
            }

            Products.Clear();
            IsBusy = true;
            ApiClient apiService = new ApiClient();

            try
            {
                var products = _productDataStore.GetProductsAsync();
                var categories = apiService.GetAsync<Category>("categories");

                await Task.WhenAll(products, categories);

                Products.Clear();
                foreach (var product in products.Result)
                {
                    Products.Add(product);
                }

                Categories.Clear();
                foreach(var category in categories.Result.Data)
                {
                    Categories.Add(category);
                }

                AddProductsInCart();
                AddProductsInSaved();
                UpdateProductListParam();
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

        public async Task OnLoadMore()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var products = await _productDataStore.GetNextPageAsync(Filters);
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

        public async Task OnApplyFilters()
        {
            
            if (IsBusy)
            {
                return;
            }

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

        private async void OnAddToCart(Product product)
        {
            AddProductsInCart();

            var existingProduct = ProductsInCart.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                ProductsInCart.Remove(existingProduct);
                product.IsInCart = false;
            }
            else
            {
                ProductsInCart.Add(product);
                product.IsInCart = true;
            }
            DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
        }

        private void OnAddToSaved(Product product)
        {
            AddProductsInSaved();

            var existingProduct = SavedProducts.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                SavedProducts.Remove(existingProduct);
                product.IsSaved = false;
            }
            else
            {
                SavedProducts.Add(product);
                product.IsSaved = true;
            }

            DataService.SaveProductsAsync(SavedProducts, "ProductsInSaved");
        }

        private void AddProductsInCart()
        {
            var productsInCart = DataService.GetProducts("ProductsInCart");
            ProductsInCart.Clear();

            foreach (var product in productsInCart)
            {
                ProductsInCart.Add(product);
            }
        }

        private void AddProductsInSaved()
        {
            var productsInSaved = DataService.GetProducts("ProductsInSaved");
            SavedProducts.Clear();

            foreach (var product in productsInSaved)
            {
                SavedProducts.Add(product);
            }
        }

        private void UpdateProductListParam()
        {
            foreach (var product in Products)
            {
                if (ProductsInCart != null)
                {
                    var changeProductInCart = ProductsInCart.FirstOrDefault(x => x.Id == product.Id);

                    if (changeProductInCart != null)
                    {
                        product.IsInCart = changeProductInCart.IsInCart;
                    }
                }
                if (SavedProducts != null)
                {
                    var changeProductInSaved = SavedProducts.FirstOrDefault(x => x.Id == product.Id);

                    if (changeProductInSaved != null)
                    {
                        product.IsSaved = changeProductInSaved.IsSaved;
                    }
                }
            }
        }
    }
}
