using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.Views.Popups;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class SavedViewModel : BaseViewModel
    {
        public ObservableCollection<Product> ProductsInCart { get; set; }
        public ObservableCollection<Product> SavedProducts { get; set; }

        public ICommand AddToCartCommand { get; }
        public ICommand AddToSavedCommand { get; }
        public ICommand ShowConfirmationCommand { get; }
        public bool IsSavedProductsEmpty => SavedProducts.Count == 0;

        public SavedViewModel()
        {
            Title = "Saved";

            ProductsInCart = new ObservableCollection<Product>();
            SavedProducts = new ObservableCollection<Product>();

            AddToCartCommand = new Command<Product>(OnAddToCart);
            AddToSavedCommand = new Command<Product>(OnAddToSaved);
            ShowConfirmationCommand = new Command(async () => await ShowConfirmationPopup());

            SavedProducts.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(IsSavedProductsEmpty));
            };
        }

        private async Task ShowConfirmationPopup()
        {
            await PopupNavigation.Instance.PushAsync(new ConfirmationPopupPage());
        }

        public void AddProductsInSaved()
        {
            var productsInSaved = DataService.GetProducts("ProductsInSaved");
            SavedProducts.Clear();

            foreach (var product in productsInSaved)
            {
                SavedProducts.Add(product);
            }
        }

        private void OnAddToCart(Product product)
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
    }
}