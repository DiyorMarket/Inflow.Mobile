using Inflow.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace Inflow.Mobile.Services
{
    public static class DataService
    {
        public static void SaveProductsAsync(IEnumerable<Product> products, string key)
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

        public static IEnumerable<Product> GetProducts(string key)
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

        public static void RemoveAllSavedProducts(string key)
        {
            try
            {
                if (SecureStorage.GetAsync(key).GetAwaiter().GetResult() != null)
                {
                    SecureStorage.Remove(key);
                    Console.WriteLine("file successfuly deleted");
                }
                else
                {
                    Console.WriteLine("file with this name cannot fined");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting: {ex.Message}");
            }
        }
    }
}
