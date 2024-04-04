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
            SendCodeCommand = new Command(OnSendCode);
            LoginCommand = new Command(OnLoginPage);
        }

        private void OnLoginPage(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnEntryCodeCommand(object obj)
        {
            var codeEntryPage = new PasswordCodeEntryPage();

            Application.Current.MainPage = codeEntryPage;
        }

        private void OnSendCode(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnNewPasswordPage(object obj)
        {
            var newPasswordPage = new NewPasswordPage();

            Application.Current.MainPage = newPasswordPage;
        }

        private void OnLoginPageNavigation(object obj)
        {
            var loginPage = new LoginPage();

            Application.Current.MainPage = loginPage;
        }
    }
}
