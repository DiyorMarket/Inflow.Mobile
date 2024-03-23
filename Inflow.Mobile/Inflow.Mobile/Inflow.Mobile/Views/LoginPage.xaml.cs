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
    public partial class LoginPage : CarouselPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Children[1];
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            CurrentPage = Children[0];
        }
    }
}