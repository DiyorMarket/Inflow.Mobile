using Inflow.Mobile.ViewModels;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseConfirmationPopupPage : PopupPage
    {
        public PurchaseConfirmationPopupPage()
        {
            InitializeComponent();
            BindingContext = new PurchaseConfirmationViewModel();
        }

        protected override async void OnAppearing()
        {
            var vm = BindingContext as CartViewModel;
            vm?.AddProductsToCart();
            base.OnAppearing();
        }

        private async void Ok_Button(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}