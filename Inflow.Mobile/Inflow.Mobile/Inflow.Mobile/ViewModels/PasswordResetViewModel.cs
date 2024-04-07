using Inflow.Mobile.Services;
using Inflow.Mobile.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Inflow.Mobile.ViewModels
{
    public class PasswordResetViewModel : BaseViewModel
    {
        private readonly LoginService _loginService;
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        private string _passwordReset;
        public string ConfirmPassword
        {
            get => _passwordReset;
            set => SetProperty(ref _passwordReset, value);
        }
        private string _codeEmail;
        public string Code
        {
            get => _codeEmail;
            set => SetProperty(ref _codeEmail, value);
        }

        public ICommand LoginPageCommand { get; }
        public ICommand CodeEntryCommand { get; }
        public ICommand NewPasswordPageCommand { get; }
        public ICommand SendCodeCommand { get; }
        public ICommand LoginCommand { get; }

        public PasswordResetViewModel() 
        {
            LoginPageCommand = new Command(OnLoginPageNavigation);
            CodeEntryCommand = new Command(OnEntryCodeCommand);
            NewPasswordPageCommand = new Command(OnNewPasswordPage);
            SendCodeCommand = new AsyncCommand<object>(OnSendCode);
            LoginCommand = new Command(OnNewPasswordPage);
            _loginService = new LoginService();
        }

        private void OnEntryCodeCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                return;
            }

            Application.Current.MainPage = new NewPasswordPage()
            {
                BindingContext = this
            };
        }

        private async Task OnSendCode(object obj)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return;
            }

            var result = _loginService.ForgotPassword(Email);

            Application.Current.MainPage = new PasswordCodeEntryPage()
            {
                BindingContext = this
            };
        }

        private void OnNewPasswordPage(object obj)
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return;
            }

            if (Password != ConfirmPassword)
            {
                return;
            }

            var result = _loginService.ResetPassword(Email, _codeEmail, Password);

            Application.Current.MainPage = new AppShell();
        }

        private void OnLoginPageNavigation(object obj)
        {
            var loginPage = new LoginPage();

            Application.Current.MainPage = loginPage;
        }
    }
}
