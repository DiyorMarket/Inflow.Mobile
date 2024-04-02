using Inflow.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Inflow.Mobile.Services
{
    public class DataService
    {
        public void SaveProductsAsync(IEnumerable<Product> products, string key)
        {
            try
            {
                var json = JsonConvert.SerializeObject(products);
                SecureStorage.SetAsync(key, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving {key}: {ex.Message}");
            }
        }

        public IEnumerable<Product> GetProducts(string key)
        {
            try
            {
                if (SecureStorage.GetAsync(key).GetAwaiter().GetResult() != null)
                {
                    var json = SecureStorage.GetAsync(key).GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting {key}: {ex.Message}");
            }
            return Enumerable.Empty<Product>();
        }
    }
}
