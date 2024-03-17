using Inflow.Mobile.ViewModels;
using System;
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
            BindingContext = new HomeViewModel();

        }

        private void ShoppingIcon_Tapped(object sender, EventArgs e)
        {
            var shoppingImage = (sender as Frame).Content as Image;

            if (shoppingImage.Source.ToString() == "basket.png")
            {
                shoppingImage.Source = "addbasket.png";
            }
            else
            {
                shoppingImage.Source = "basket.png";
            }
        }
    }
}