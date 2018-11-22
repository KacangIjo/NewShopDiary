using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using System.Net.Http.Headers;
using Newtonsoft.Json;

using ShopDiaryAbb.Helper;
using ShopDiaryAbb.Models.ViewModels;
using ShopDiaryAbb.Models;

namespace ShopDiaryAbb.Services
{
    public class ShopItemDataService
    {
        //JANGAN LUPA GANTI Shopitem PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<ShopitemViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Shopitem_Url + @"/GetStorages"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<ShopitemViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Shopitem data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ItemName", data.ItemName.ToString()),
                new KeyValuePair<string, string>("Quantity", data.Quantity.ToString()),
                new KeyValuePair<string, string>("Price", data.Price.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("ProductId", data.ProductId.ToString()),
                new KeyValuePair<string, string>("ShoplistId", data.ShoplistId.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.CreatedUserId.ToString()),
            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Shopitem_Url + @"/PostStorage", content).Result;
                Shopitem t = JsonConvert.DeserializeObject<Shopitem>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Shopitem data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ItemName", data.ItemName.ToString()),
                new KeyValuePair<string, string>("Quantity", data.Quantity.ToString()),
                new KeyValuePair<string, string>("Price", data.Price.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("ProductId", data.ProductId.ToString()),
                new KeyValuePair<string, string>("ShoplistId", data.ShoplistId.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.CreatedUserId.ToString()),

            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Shopitem_Url + @"/PutStorage/" + id, content).Result;
                Shopitem t = JsonConvert.DeserializeObject<Shopitem>(resp.Content.ReadAsStringAsync().Result);
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

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Shopitem_Url + @"/DeleteStorage/" + id).Result;
                Shopitem t = JsonConvert.DeserializeObject<Shopitem>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    