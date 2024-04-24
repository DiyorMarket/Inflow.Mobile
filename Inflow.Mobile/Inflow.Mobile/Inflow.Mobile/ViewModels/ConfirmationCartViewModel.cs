using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class ConfirmationCartViewModel : BaseViewModel
    {
        public ICommand ClearAllCartProductsCommand { get; }
        public ICommand CancelCommand { get; }
        public ObservableCollection<Product> ProductsInCart { get; set; }

        public ConfirmationCartViewModel() 
        {
            ClearAllCartProductsCommand = new Command(async () => await ClearAllProductsInCart());
            CancelCommand = new Command(async () => await Cancel());
            ProductsInCart = new ObservableCollection<Product>();
        }

        private async Task ClearAllProductsInCart()
        {
            ProductsInCart.Clear();
            DataService.SaveProductsAsync(ProductsInCart, "ProductsInCart");
            await PopupNavigation.Instance.PopAsync();
            MessagingCenter.Send(this, "CartUpdated");
        }


        private async Task Cancel()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }
    }
}
