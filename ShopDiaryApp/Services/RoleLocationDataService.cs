using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShopDiaryProject.Domain.Models;
using ShopDiaryApp.Models.ViewModels;
using ShopDiaryApp.Helper;

namespace ShopDiaryApp.Services
{
    public class RoleLocationDataService
    {
        //JANGAN LUPA GANTI LOCATION PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<RoleLocationViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Userlocations_Url + @"/getLocations"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<RoleLocationViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(RoleLocation data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", data.Id.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("RoleCode", data.RoleCode.ToString()),
              


            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Userlocations_Url + @"/PostLocation", content).Result;
                RoleLocation t = JsonConvert.DeserializeObject<RoleLocation>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, RoleLocation data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("RoleCode", data.RoleCode.ToString()),


            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Userlocations_Url + @"/PutLocation/" + id, content).Result;
                RoleLocation t = JsonConvert.DeserializeObject<RoleLocation>(resp.Content.ReadAsStringAsync().Result);
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

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Userlocations_Url + @"/DeleteLocation/" + id).Result;
                RoleLocation t = JsonConvert.DeserializeObject<RoleLocation>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    