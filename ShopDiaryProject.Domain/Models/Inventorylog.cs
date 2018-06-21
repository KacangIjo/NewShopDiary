using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Inventorylog : FullAuditedEntity
    {

        public DateTime? LogDate { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public Inventorylog()
        {
           

        }
    }
}