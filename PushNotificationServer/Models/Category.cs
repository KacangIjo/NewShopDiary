using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PushNotificationServer.Models
{
    public class Category : FullAuditedEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        

    }
}