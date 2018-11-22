using System;

namespace PushNotificationServer.Models
{
    public class Inventorylog : FullAuditedEntity
    {

        public DateTime? LogDate { get; set; }
        public string Description { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public Inventorylog()
        {
           

        }
    }
}