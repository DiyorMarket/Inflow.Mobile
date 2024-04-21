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
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to close the modal: " + ex.Message);
            }
        }
    }
}
