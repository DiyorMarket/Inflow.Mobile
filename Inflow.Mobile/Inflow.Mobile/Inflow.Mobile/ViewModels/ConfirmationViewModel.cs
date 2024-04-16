using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class ConfirmationViewModel : BaseViewModel
    {
        public ICommand ClearAllSavedProductsCommand { get; }
        public ICommand CancelCommand { get; }
        public ObservableCollection<Product> SavedProducts { get; set; }

        public ConfirmationViewModel()
        {
            ClearAllSavedProductsCommand = new Command(async () => await ClearAllSavedProducts());
            CancelCommand = new Command(async () => await Cancel());
            SavedProducts = new ObservableCollection<Product>();
        }

        private async Task ClearAllSavedProducts()
        {
            var productsInSaved = DataService.GetProducts("ProductsInSaved");
            SavedProducts.Clear();

            foreach (var product in productsInSaved)
            {
                SavedProducts.Add(product);
            }

            SavedProducts.Clear();

            DataService.SaveProductsAsync(SavedProducts, "ProductsInSaved");

            await PopupNavigation.Instance.PopAsync();

            await Application.Current.MainPage.Navigation.PushAsync(new SavedPage());
        }


        private async Task Cancel()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync(); // Закрыть всплывающее окно
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }

    }
}
