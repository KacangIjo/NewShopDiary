using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using Newtonsoft.Json;
using PushNotificationServer.Models;
using PushNotificationServer.Helper;
using PushNotificationServer.Models.ViewModels;

namespace PushNotificationServer.Services
{
    public class InventoryDataService
    {
        //JANGAN LUPA GANTI Inventory PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<InventoryViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Inventories_Url + @"/GetInventories"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<InventoryViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Inventory data)
        {
            string expirationDate = string.Format("{0}-{1}-{2}T00:00:00", data.ExpirationDate.Year, data.ExpirationDate.Month, data.ExpirationDate.Day);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ExpirationDate", expirationDate),
                new KeyValuePair<string, string>("Price", data.Price.ToString()),             
                new KeyValuePair<string, string>("ProductId", data.ProductId.ToString()),
                new KeyValuePair<string, string>("StorageId", data.StorageId.ToString()),
                new KeyValuePair<string, string>("IsDeleted", false.ToString()),
                new KeyValuePair<string, string>("IsConsumed", false.ToString()),
                 new KeyValuePair<string, string>("ItemName","-"),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString())
            });
            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Inventories_Url + @"/PostInventory", content).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ConsumeInv(Guid id, Inventory data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("IsConsumed", true.ToString()),
            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Inventories_Url + @"/PutInventory/" + id, content).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Inventory data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Price", data.Price.ToString()),
                new KeyValuePair<string, string>("ExpDate", data.ExpirationDate.ToString()),
                new KeyValuePair<string, string>("InventoryId", data.Id.ToString()),


            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Inventories_Url + @"/PutInventory/" + id, content).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Inventories_Url + @"/DeleteInventory/" + id).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    