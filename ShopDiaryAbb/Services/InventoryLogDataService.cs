using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShopDiaryProject.Domain.Models;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Helper;

namespace ShopDiaryAbb.Services
{
    public class InventoryLogDataService
    {
        //JANGAN LUPA GANTI Inventory PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<InventorylogViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Inventorylogs_Url + @"/GetInventories"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<InventorylogViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Inventorylog data)
        {
            
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("InventoryId", data.InventoryId.ToString()),
                new KeyValuePair<string, string>("CreatedDate", data.CreatedDate.ToString()),             
                new KeyValuePair<string, string>("LogDate",  data.LogDate.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString())
                 
            });
            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Inventorylogs_Url + @"/PostInventory", content).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ConsumeInv(Guid id, Inventorylog data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Description", "Consumed By "+data.AddedUserId),
            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Inventorylogs_Url + @"/PutInventory/" + id, content).Result;
                Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public bool Edit(Guid id, Inventorylog data)
        //{
        //    var content = new FormUrlEncodedContent(new[]
        //    {
        //        new KeyValuePair<string, string>("Price", data.Price.ToString()),
        //        new KeyValuePair<string, string>("ExpDate", data.ExpirationDate.ToString()),
        //        new KeyValuePair<string, string>("InventoryId", data.Id.ToString()),

        //    });

        //    try
        //    {
        //        HttpResponseMessage resp = client.PutAsync(UrlHelper.Inventorylogs_Url + @"/PutInventory/" + id, content).Result;
        //        Inventory t = JsonConvert.DeserializeObject<Inventory>(resp.Content.ReadAsStringAsync().Result);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool Delete(Guid id)
        {
            try
            {

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Inventorylogs_Url + @"/DeleteInventory/" + id).Result;
                Inventorylog t = JsonConvert.DeserializeObject<Inventorylog>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    