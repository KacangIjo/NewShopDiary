using System;
using System.Collections.Generic;

namespace PushNotificationServer.Models
{
    public class Location : FullAuditedEntity
    {

       
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

      
        public Guid UserId { get; set; }

        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public Location()
        {
            

        }
       
    }
}