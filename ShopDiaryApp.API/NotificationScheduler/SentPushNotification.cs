using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ShopDiaryApp.API.NotificationScheduler.JsonObjectNotification;

namespace ShopDiaryApp.API.NotificationScheduler
{
    public class SentPushNotification : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var push = new Push
            {
                Content = new Content
                {
                    Name = "Campaign Name",
                    Title = "Expired Warning",
                    Body = "You have items that almost expired"
                }
            };
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                var content = new StringContent(push.ToString(), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("X-API-Token", "b547b248fe7321c091f0027116a0aa84569eff85");
                var builder = new UriBuilder(new Uri("https://appcenter.ms/api/v0.1/apps/KacangIjo/ShopDiaryApp/push/notifications"));
                var response = await client.PostAsync(builder.Uri, content);

            }
            
        }
 
    }
}