using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Purchase : FullAuditedEntity
    {

        public DateTime PurchaseDate { get; set; }
        [MaxLength(250)]
        public string Market { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public Purchase()
        {

        }
    }
}