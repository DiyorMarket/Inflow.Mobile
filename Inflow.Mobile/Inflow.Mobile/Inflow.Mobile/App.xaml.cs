using Inflow.Mobile.ViewModels;
using Inflow.Mobile.Views;
using Xamarin.Forms;

namespace Inflow.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            (MainPage.BindingContext as CartViewModel)?.SaveProductsAsync();
        }

        protected override void OnResume()
        {
        }
    }
}
