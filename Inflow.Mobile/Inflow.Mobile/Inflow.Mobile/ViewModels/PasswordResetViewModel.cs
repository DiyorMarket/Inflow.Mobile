using Inflow.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class PasswordResetViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }

        public PasswordResetViewModel() 
        {
            LoginCommand = new Command(OnLoginPage);
        }

        private void OnLoginPage(object obj)
        {
            var loginPage = new LoginPage();

            Application.Current.MainPage = loginPage;
        }
    }
}
