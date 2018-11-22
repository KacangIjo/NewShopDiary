    using System;


namespace PushNotificationServer.Models
{
    public class UserData : FullAuditedEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public UserData()
        {
            

        }
       
    }
}