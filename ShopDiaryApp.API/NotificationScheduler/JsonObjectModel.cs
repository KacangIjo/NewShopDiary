using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryApp.API.Models
{
    public class JsonObjectModel
    {
        public class NotificationContent
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }
        }

        public class PostObject
        {
            [JsonProperty("notification_content")]
            public NotificationContent NotificationContent { get; set; }
        }
    }
}
