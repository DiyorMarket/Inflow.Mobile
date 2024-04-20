using Inflow.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Customers
{
    public interface ICustomerDataStore
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(int? userId);
    }
}
