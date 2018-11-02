using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class Consume : FullAuditedEntity
    {

        public DateTime DateConsumed { get; set; }

        public Boolean IsConsumed { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        
    }
}