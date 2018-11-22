using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class Shopitem : FullAuditedEntity
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public Product product { get; set; }
        public Guid ShoplistId { get; set; }
        public Shoplist Shoplist { get; set; }
        
        public Shopitem()
        {
            ItemName = "";
            Quantity=0;
            Price = 0.0M;
            Description = "";
        }

    }
}