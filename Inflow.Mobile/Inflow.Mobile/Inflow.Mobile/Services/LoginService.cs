using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                    login = email,
                    password
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/login", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<string>(resultJson);

                return string.IsNullOrEmpty(token);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RegisterUser(string email, string userName, string phoneNumber, string password)
        {
            try
            {
                var body = new
                {
                    login = email,
                    password,
                    userName,
                    phoneNumber
                };
                var jsonBody = JsonConvert.SerializeObject(body);

                var result = await _client.PostAsync<bool>("auth/register", jsonBody);
                result.EnsureSuccessStatusCode();

                var resultJson = await result.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<string>(resultJson);

                return string.IsNullOrEmpty(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
