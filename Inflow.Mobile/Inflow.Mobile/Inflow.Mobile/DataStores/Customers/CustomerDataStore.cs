using Inflow.Mobile.Models;
using Inflow.Mobile.Responses;
using Inflow.Mobile.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Customers
{
    public class CustomerDataStore : ICustomerDataStore
    {
        public readonly ApiClient _client;

        public CustomerDataStore(ApiClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(int? userId)
        {
            ApiResponse<Customer> response;

            if(userId != null)
            {
                response = await _client.GetAsync<Customer>($"Customers?UserId={userId}");

                return response.Data;
            }
            response = await _client.GetAsync<Customer>($"Customers");

            return response.Data;
        }
    }
}
