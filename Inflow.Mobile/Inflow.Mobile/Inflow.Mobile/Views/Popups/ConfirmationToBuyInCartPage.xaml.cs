using Inflow.Mobile.DataStores.Customers;
using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmationToBuyInCartPopupPage : PopupPage
    {
        public ConfirmationToBuyInCartPopupPage()
        {
            InitializeComponent();
            var apiClient = new ApiClient();
            var saleDataStore = new SaleDataStore(apiClient);
            var custoemrDataStire = new CustomerDataStore(apiClient);
            BindingContext = new ConfirmationToBuyInCartViewModel(saleDataStore, custoemrDataStire);
        }
    }
}