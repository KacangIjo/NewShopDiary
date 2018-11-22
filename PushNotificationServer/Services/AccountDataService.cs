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
    public class AccountDataService
    {
        private HttpClient client = new HttpClient();
        public async Task<UserInfoViewModel> GetUserInfo()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                   

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", LoginHelper.Token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Account_Url + @"/UserInfo"));
                    var response = await client.GetAsync(builder.Uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<UserInfoViewModel>(result);
                    return modelResult;
                }
                catch (Exception)
                {

                    return null;
                }
            }
        }
        public async Task<bool> Login(string username, string password)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
            });

            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(UrlHelper.Account_Login));
                    var response = await client.PostAsync(builder.Uri, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    var byteResult = await response.Content.ReadAsByteArrayAsync();
                    var result = Encoding.UTF8.GetString(byteResult, 0, byteResult.Length);

                    var modelResult = JsonConvert.DeserializeObject<AccountViewModel>(result);
                    LoginHelper.Username = modelResult.Username;

                    LoginHelper.Token = modelResult.AccessToken;
                    return true;
                }
                catch (Exception )
                {

                    return false;
                }
            }
        }

        public bool Logout()
        {
            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Account_Url + @"/Logout", null).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangePassword(string oldPassword, string newPassword, string confPassword)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("OldPassword", oldPassword),
                new KeyValuePair<string, string>("NewPassword", newPassword),
                new KeyValuePair<string, string>("ConfirmPassword", confPassword),

            });

            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Account_Url + @"/ChangePassword", content).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetPassword(string newPassword, string confPassword)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("NewPassword", newPassword),
                new KeyValuePair<string, string>("ConfirmPassword", confPassword),

            });

            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Account_Url + @"/SetPassword", content).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Register(string email, string password, string confPassword)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", email),
                new KeyValuePair<string, string>("Password", password),
                new KeyValuePair<string, string>("ConfirmPassword", confPassword),

            });

            try
            {
                HttpResponseMessage resp = client.PostAsync(UrlHelper.Account_Url + @"/Register", content).Result;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}