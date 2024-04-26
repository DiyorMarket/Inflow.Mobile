using Inflow.Mobile.Models;
using Inflow.Mobile.Services;
using Inflow.Mobile.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

namespace Inflow.Mobile.ViewModels
{
    public class AccountViewModel
    {
        private readonly LoginService _loginService;
        public AuthenticationResponse LoginResponse { get; private set; }
        public ICommand RegisterCommand { get; }
        public AccountViewModel()
        {
            LoginResponse = new AuthenticationResponse();
            _loginService = new LoginService();
            RegisterCommand = new Command(NavigationLoginPage);
            GetUserData();
        }

        private void NavigationLoginPage()
        {
            _loginService.DeleteUserData();

            Application.Current.MainPage = new LoginPage();
        }

        private async void GetUserData()
        {
            try
            {
                var data = _loginService.GetUserData();
                LoginResponse.UserName = data.Result.UserName;
                LoginResponse.UserEmail = data.Result.UserEmail;
                LoginResponse.UserPhone = data.Result.UserPhone;
            }
            catch (Exception ex)
            {
                throw new Exception("Some thing is wrong", ex);
            }
        }
    }
}
