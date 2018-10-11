using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShopDiaryAndroid.Helper;
using System.Net.Http;
using System.Net.Http.Headers;
using ShopDiaryProject.Domain.Models;
using ShopDiaryAndroid.Models.ViewModels;


namespace ShopDiaryAndroid.Services
{
    public class ConsumeDataService
    {
        //JANGAN LUPA GANTI Category PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<ConsumeViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Consumes_Url + @"/GetConsumes"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<ConsumeViewModel>>(result);
                    return modelResult;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }

        public bool Add(Consume data)
        {
            string ConsumedDate = string.Format("{0}-{1}-{2}T00:00:00", data.DateConsumed.Year, data.DateConsumed.Month, data.DateConsumed.Day);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("DateConsumed", ConsumedDate.ToString()),
                new KeyValuePair<string, string>("InventoryId", data.InventoryId.ToString())

            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Consumes_Url + @"/PostConsume", content).Result;
                Consume t = JsonConvert.DeserializeObject<Consume>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Consume data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("DateConsumed", data.DateConsumed.ToString()),
                new KeyValuePair<string, string>("InventoryId", data.InventoryId.ToString()),

            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Consumes_Url + @"/PutConsume/" + id, content).Result;
                Consume t = JsonConvert.DeserializeObject<Consume>(resp.Content.ReadAsStringAsync().Result);
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

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Consumes_Url + @"/DeleteConsume/" + id).Result;
                Consume t = JsonConvert.DeserializeObject<Consume>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    