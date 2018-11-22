using Newtonsoft.Json;
using PushNotificationServer.NotificationScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static PushNotificationServer.Models.JsonObjectModel;
using static PushNotificationServer.NotificationScheduler.JsonObjectNotification;

namespace PushNotificationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu = 1;
            while (menu != 0)
            {
                JobScheduler.StartAsync().GetAwaiter().GetResult();
                Console.WriteLine("berhasil");
                menu = int.Parse(Console.ReadLine());
            }




        }
        private static async Task RunAsync()
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                PostObject postObject = new PostObject
                {
                    NotificationContent = new NotificationContent
                    {
                        Name = "Campaign Name",
                        Title = "Expired Warning",
                        Body = "You have items that almost expired"
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
