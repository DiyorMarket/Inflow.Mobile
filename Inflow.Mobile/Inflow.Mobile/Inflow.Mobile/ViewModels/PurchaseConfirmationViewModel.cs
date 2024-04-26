using Inflow.Mobile.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class PurchaseConfirmationViewModel : BaseViewModel
    {
        public ICommand OKCommand { get; }

        public PurchaseConfirmationViewModel()
        {
            OKCommand = new Command(async () => await OK());
        }

        public async Task OK()
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }
    }
}
