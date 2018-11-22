using System;
using System.Collections.Generic;


namespace PushNotificationServer.Models
{
    public class Inventory : FullAuditedEntity
    {
        public DateTime ExpirationDate { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public bool IsConsumed { get; set; }

        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<Inventorylog> Inventorylogs { get; set; }
        public Inventory()
        {

        }
    }
}