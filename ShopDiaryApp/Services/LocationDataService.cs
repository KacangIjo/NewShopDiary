using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShopDiaryApp.Helper;
using ShopDiaryProject.Domain.Models;
using ShopDiaryApp.Models.ViewModels;

namespace ShopDiaryApp.Services
{
    public class LocationDataService
    {
        //JANGAN LUPA GANTI LOCATION PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<LocationViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Locations_Url + @"/getLocations"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<LocationViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(Location data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", data.Id.ToString()),
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("Address", data.Address.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("CreatedUserId", data.CreatedUserId.ToString()),

            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Locations_Url + @"/PostLocation", content).Result;
                Location t = JsonConvert.DeserializeObject<Location>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, Location data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("Id", data.Id.ToString()),
                new KeyValuePair<string, string>("Name", data.Name.ToString()),
                new KeyValuePair<string, string>("Address", data.Address.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("CreatedUserId", data.CreatedUserId.ToString()),


            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Locations_Url + @"/PutLocation/" + id, content).Result;
                Location t = JsonConvert.DeserializeObject<Location>(resp.Content.ReadAsStringAsync().Result);
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

                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.Locations_Url + @"/DeleteLocation/" + id).Result;
                Location t = JsonConvert.DeserializeObject<Location>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    