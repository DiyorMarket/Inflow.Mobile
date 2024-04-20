using Inflow.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Inflow.Mobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmationCartPopupPage : PopupPage
    {
		public ConfirmationCartPopupPage()
		{
            InitializeComponent();
			BindingContext = new ConfirmationCartViewModel();
		}
	}
}