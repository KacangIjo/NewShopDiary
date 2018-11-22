using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ShopDiaryApp.API.Models.JsonObjectModel;
using static ShopDiaryApp.API.NotificationScheduler.JsonObjectNotification;

namespace ShopDiaryApp.API.NotificationScheduler
{
    public class SentPushNotification : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                PostObject postObject = new PostObject
                {
                    NotificationContent = new NotificationContent
                    {
                        Name = "Campaign Name",
                        Title = "Expired Warning",
                        Body = "Check if you have expired item in your inventory"
                    }
                };

                var myContent = JsonConvert.SerializeObject(postObject);
                client.DefaultRequestHeaders.Add("X-API-Token", "b547b248fe7321c091f0027116a0aa84569eff85");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var builder = new UriBuilder(new Uri("https://appcenter.ms/api/v0.1/apps/KacangIjo/ShopDiaryApp/push/notifications"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
                request.Content = new StringContent(myContent, Encoding.UTF8, "application/json");//CONTENT-TYPE header

                HttpResponseMessage response = await client.SendAsync(request);
            };

        }
 
    }
}