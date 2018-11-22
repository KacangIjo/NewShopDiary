using System;
using System.Collections.Generic;

namespace PushNotificationServer.Models
{
    public class Storage : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public Storage()
        {
           

        }
    }
}