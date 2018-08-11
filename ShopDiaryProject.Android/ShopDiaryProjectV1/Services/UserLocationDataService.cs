using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ShopDiaryProjectV1.Helper;
using Newtonsoft.Json;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Android.Models.ViewModels;

namespace ShopDiaryProjectV1.Services
{
    public class UserLocationDataService
    {
        //JANGAN LUPA GANTI LOCATION PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<UserLocationViewModel>> GetAll()
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

                    var modelResult = JsonConvert.DeserializeObject<List<UserLocationViewModel>>(result);
                    return modelResult;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        public bool Add(UserLocation data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Id", data.Id.ToString()),
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
             
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("LocationId", data.LocationId.ToString()),


            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.Userlocations_Url + @"/PostLocation", content).Result;
                UserLocation t = JsonConvert.DeserializeObject<UserLocation>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, UserLocation data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Description", data.Description.ToString()),
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
               
                new KeyValuePair<string, string>("AddedUserId", data.AddedUserId.ToString()),
                new KeyValuePair<string, string>("LocationId", data.LocationId.ToString()),


            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.Userlocations_Url + @"/PutLocation/" + id, content).Result;
                UserLocation t = JsonConvert.DeserializeObject<UserLocation>(resp.Content.ReadAsStringAsync().Result);
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
                UserLocation t = JsonConvert.DeserializeObject<UserLocation>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    