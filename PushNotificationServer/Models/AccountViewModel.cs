using System;
using Newtonsoft.Json;

namespace PushNotificationServer.Models
{
    public class AccountViewModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiredIn { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = ".issued")]
        public DateTimeOffset LoginDate { get; set; }
        [JsonProperty(PropertyName = ".expires")]
        public DateTimeOffset ExpiredDate { get; set; }
    }
}