using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushNotificationServer.NotificationScheduler
{
    public class JsonObjectNotification
    {
        [JsonObject]
        public class Push
        {
            [JsonProperty("notification_target")]
            public Target Target { get; set; }

            [JsonProperty("notification_content")]
            public Content Content { get; set; }
        }

        [JsonObject]
        public class Content
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("custom_data")]
            public IDictionary<string, string> Payload { get; set; }
        }

        [JsonObject]
        public class Target
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("devices")]
            public IEnumerable<string> Devices { get; set; }
        }

        public class Constants
        {
            public const string Url = "https://api.appcenter.ms/v0.1/apps/";
            public const string ApiKeyName = "X-API-Token";
            public const string ApiKey = "b547b248fe7321c091f0027116a0aa84569eff85";
            public const string Organization = "KacangIjo";
            public const string Android = "ShopDiaryApp";
            public const string DeviceTarget = "devices_target";
            public class Apis { public const string Notification = "push/notifications"; }
        }
    }
}