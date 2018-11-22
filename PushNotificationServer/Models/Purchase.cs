using System;


namespace PushNotificationServer.Models
{
    public class Purchase : FullAuditedEntity
    {

        public DateTime PurchaseDate { get; set; }
        public string Store { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public Purchase()
        {

        }
    }
}