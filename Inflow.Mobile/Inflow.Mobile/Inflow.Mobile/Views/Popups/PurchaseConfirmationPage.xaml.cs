using Inflow.Mobile.ViewModels;
using Inflow.Mobile.ViewModels.Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseConfirmationPopupPage : PopupPage
    {
        public PurchaseConfirmationPopupPage()
        {
            InitializeComponent();
            AnimateImage();
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
        private async void AnimateImage()
        {
            await SuccessImage.ScaleTo(1.5, 1000, Easing.Linear);

            await SuccessImage.RotateTo(360, 1000, Easing.Linear);

            await SuccessImage.ScaleTo(1, 1000, Easing.Linear);
        }
    }
}