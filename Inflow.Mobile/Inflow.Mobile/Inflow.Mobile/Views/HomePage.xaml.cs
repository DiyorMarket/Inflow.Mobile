using Inflow.Mobile.DataStores.Products;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent(); 
            var apiClient = new ApiClient();
            var productDataStore = new ProductDataStore(apiClient);
            BindingContext = new HomeViewModel(productDataStore);

        }
    }
}