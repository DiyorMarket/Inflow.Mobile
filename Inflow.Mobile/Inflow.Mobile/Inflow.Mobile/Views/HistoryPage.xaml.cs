using Inflow.Mobile.DataStores.Sales;
using Inflow.Mobile.Services;
using Inflow.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            var api = new ApiClient();
            var saleDataStore = new SaleDataStore(api);

            BindingContext = new HistoryViewModel(saleDataStore);
        }
        protected override async void OnAppearing()
        {
            var vm = BindingContext as HistoryViewModel;
            await vm?.LoadSaleHistory();

            base.OnAppearing();
        }
    }
}