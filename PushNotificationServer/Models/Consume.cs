using System;


namespace PushNotificationServer.Models
{
    public class Consume : FullAuditedEntity
    {

        public DateTime DateConsumed { get; set; }

        public Boolean IsConsumed { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        
    }
}