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
    public class ShoplistDataService
    {
        //JANGAN LUPA GANTI Shoplist PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<ShoplistViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.ShopList_Url + @"/GetShoplists"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<ShoplistViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Shoplist data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("LocationId", data.LocationId.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.CreatedUserId.ToString()),
            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.ShopList_Url + @"/PostShoplist", content).Result;
                Shoplist t = JsonConvert.DeserializeObject<Shoplist>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Shoplist data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("LocationId", data.LocationId.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.CreatedUserId.ToString()),

            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.ShopList_Url + @"/PutShopList/" + id, content).Result;
                Shoplist t = JsonConvert.DeserializeObject<Shoplist>(resp.Content.ReadAsStringAsync().Result);
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

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.ShopList_Url + @"/DeleteShopList/" + id).Result;
                Shoplist t = JsonConvert.DeserializeObject<Shoplist>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    