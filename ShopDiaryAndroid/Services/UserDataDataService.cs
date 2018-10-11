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
    public class UserDataDataService
    {
        //JANGAN LUPA GANTI Storage PAKE .DOMAIN
        private HttpClient client = new HttpClient();
        public async Task<List<UserDataViewModel>> GetAll()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.UserDatas_Url + @"/GetUserDatas"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<List<UserDataViewModel>>(result);
                    return modelResult;
                }
                catch (Exception )
                {

                    return null;
                }
            }
        }

        public bool Add(UserData data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserId", data.UserId.ToString()),
                new KeyValuePair<string, string>("Email", data.Email.ToString()),
            });

            try
            {

                HttpResponseMessage resp = client.PostAsync(UrlHelper.UserDatas_Url + @"/PostUserDatas", content).Result;
                UserData t = JsonConvert.DeserializeObject<UserData>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(Guid id, UserData data)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserId", data.UserId.ToString()),
                new KeyValuePair<string, string>("Email", data.Email.ToString()),
            });

            try
            {
                HttpResponseMessage resp = client.PutAsync(UrlHelper.UserDatas_Url + @"/PutUserData/" + id, content).Result;
                UserData t = JsonConvert.DeserializeObject<UserData>(resp.Content.ReadAsStringAsync().Result);
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
                HttpResponseMessage resp = client.DeleteAsync(UrlHelper.UserDatas_Url + @"/DeleteUserData/" + id).Result;
                UserData t = JsonConvert.DeserializeObject<UserData>(resp.Content.ReadAsStringAsync().Result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
    