using Inflow.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inflow.Mobile.DataStores.Sales
{
    public interface ISaleDataStore
    {
        Task<IEnumerable<Sale>> GetSales(int CustomerId);
    }
}
