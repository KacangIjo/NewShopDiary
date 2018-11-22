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
    public class ProductDataService
    {
        //JANGAN LUPA GANTI Product PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<ProductViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Products_Url + @"/GetProducts"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Product data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("BarcodeId", data.BarcodeId.ToString()),
                new KeyValuePair<string, string>("CategoryId", data.CategoryId.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Products_Url + @"/PostProduct", content).Result;
                Product t = JsonConvert.DeserializeObject<Product>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Product data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("SeatSize", data.BarcodeId.ToString()),
                new KeyValuePair<string, string>("ProductId", data.Id.ToString()),
            });
            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Products_Url + @"/PutProduct/" + id, content).Result;
                Product t = JsonConvert.DeserializeObject<Product>(resp.Content.ReadAsStringAsync().Result);
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
                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Products_Url + @"/DeleteProduct/" + id).Result;
                Product t = JsonConvert.DeserializeObject<Product>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    