using Inflow.Mobile.ViewModels;
using Xamarin.Forms;

namespace Inflow.Mobile.Views
{
    public partial class SavedPage : ContentPage
    {
        SavedViewModel _viewModel;

        public SavedPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SavedViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.AddProductsInSaved();
            base.OnAppearing();
        }
    }
}