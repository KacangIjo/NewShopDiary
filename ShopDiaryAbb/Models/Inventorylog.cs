using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
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