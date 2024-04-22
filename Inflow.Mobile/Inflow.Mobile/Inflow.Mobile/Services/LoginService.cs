using Inflow.Mobile.Models;
using Inflow.Mobile.Views;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

                var response = JsonConvert.DeserializeObject<AuthenticationResponse>(resultJson);

                bool isAuthenticate = !string.IsNullOrEmpty(response.Token);

                if (isAuthenticate)
                {
                    SaveUserData(response.Token, response.UserId, response.UserName, response.UserEmail, response.UserPhone);
                }

                return isAuthenticate;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
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
                    Phone = phoneNumber,
                    Role = "customer"
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/register", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content.ReadAsStringAsync();

                var response = JsonConvert.DeserializeObject<AuthenticationResponse>(resultJson);

                bool isAuthenticate = !string.IsNullOrEmpty(response.Token);

                if (isAuthenticate)
                {
                    SaveUserData(response.Token, response.UserId, response.UserName, response.UserEmail, response.UserPhone);
                }

                return isAuthenticate;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
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
                return false;
            }
            catch (Exception ex)
            {
                return false;
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

                var response = JsonConvert.DeserializeObject<AuthenticationResponse>(resultJson);

                bool isAuthenticate = !string.IsNullOrEmpty(response.Token);

                if (isAuthenticate)
                {
                    SaveUserData(response.Token, response.UserId, response.UserName, response.UserEmail, response.UserPhone);
                }

                return isAuthenticate;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task SaveUserData(string token, int userId, string userName, string userEmail, string userPhone)
        {
            try
            {
                await SecureStorage.SetAsync("AuthToken", token);
                await SecureStorage.SetAsync("UserId", userId.ToString());
                await SecureStorage.SetAsync("UserName", userName);
                await SecureStorage.SetAsync("UserEmail", userEmail);
                await SecureStorage.SetAsync("UserPhone", userPhone);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while saving user data:", ex);
            }
        }

        public async Task<AuthenticationResponse> GetUserData()
        {
            try
            {
                string token = await SecureStorage.GetAsync("AuthToken");
                string userIdStr = await SecureStorage.GetAsync("UserId");
                int userId = int.Parse(userIdStr);
                string userName = await SecureStorage.GetAsync("UserName");
                string userEmail = await SecureStorage.GetAsync("UserEmail");
                string userPhone = await SecureStorage.GetAsync("UserPhone");

                return new AuthenticationResponse
                {
                    Token = token,
                    UserId = userId,
                    UserName = userName,
                    UserEmail = userEmail,
                    UserPhone = userPhone
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Data is don't exists", ex);
            }
        }
    }
}
