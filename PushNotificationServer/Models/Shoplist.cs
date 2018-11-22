using System;
using System.Collections.Generic;

namespace PushNotificationServer.Models
{
    public class Shoplist : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Store { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Shopitem> ShopItems { get; set; }
        public Shoplist()
        {
           

        }
    }
}