using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Inflow.Mobile.Services
{
    internal class LoginService
    {
        private readonly ApiClient _client;

        public LoginService()
        {
            _client = new ApiClient();
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                var body = new
                {
                    Login = email,
                    Password = password
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/login", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<string>(resultJson);

                return string.IsNullOrEmpty(token);
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            finally
            {
                Application.Current.MainPage = new AppShell();
            }
        }

        public async Task<bool> RegisterUser(string email, string userName, string phoneNumber, string password)
        {
            try
            {
                var body = new
                {
                    Login = email,
                    Password = password,
                    FullName = userName,
                    Phone = phoneNumber
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/register", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<string>(resultJson);

                return string.IsNullOrEmpty(token);
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            finally
            {
                Application.Current.MainPage = new AppShell();
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var body = new
                {
                    Login = email
                };
                var jsobBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/forgotPassword", jsobBody);
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            finally
            {
                Application.Current.MainPage = new AppShell();
            }
        }

        public async Task<bool> ResetPassword(string email, string resetCode, string newPassword)
        {
            try
            {
                var body = new
                {
                    Login = email,
                    ResetCode = resetCode,
                    newPassword = newPassword
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/resetPassword", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content?.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<string>(resultJson);

                return string.IsNullOrEmpty(token);
            }
            catch (HttpRequestException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred. Please try again later.", "OK");
                return false;
            }
            finally
            {
                Application.Current.MainPage = new AppShell();
            }
        }
    }
}
