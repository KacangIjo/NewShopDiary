using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class Product : FullAuditedEntity
    {
        
        public string Name { get; set; }
        public string BarcodeId { get; set; }



        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Shopitem> ShopItems { get; set; }
        public Product()
        {
            Name = "";
            BarcodeId = "";
            CategoryId = new Guid();
        }

    }
}