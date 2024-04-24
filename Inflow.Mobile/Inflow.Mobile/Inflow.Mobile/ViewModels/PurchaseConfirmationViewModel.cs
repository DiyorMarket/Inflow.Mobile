using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Inflow.Mobile.ViewModels
{
    public class PurchaseConfirmationViewModel : BaseViewModel
    {
        public ICommand OKCommand { get; }
        private CartViewModel _cart;

        public PurchaseConfirmationViewModel()
        {
            OKCommand = new Command(async () => await OK());
            _cart = new CartViewModel();
        }

        public async Task OK()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new PersonalAccountPage());
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }
    }
}
