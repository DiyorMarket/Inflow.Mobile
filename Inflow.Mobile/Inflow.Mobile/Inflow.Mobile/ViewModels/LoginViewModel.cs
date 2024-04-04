using Inflow.Mobile.Services;
using Inflow.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Inflow.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly LoginService _loginService;

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _showCancel;
        public bool ShowCancel
        {
            get { return _showCancel; }
            set { SetProperty(ref _showCancel, value); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand CancelLoginCommand { get; }

        public LoginViewModel() 
        {
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            ForgotPasswordCommand = new Command(OnResetPassword);
            _loginService = new LoginService();
        }
        private void OnResetPassword(object obj)
        {
            var resetPasswordPage = new PasswordResetPage();

            Application.Current.MainPage = resetPasswordPage;
        }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var result = _loginService.LoginUser(Username, Password);

            Application.Current.MainPage = new AppShell();
        }

        private async void OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(Username))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var result = _loginService.RegisterUser(Email, Password, Username, PhoneNumber);

            Application.Current.MainPage= new AppShell();
        }
    }
}