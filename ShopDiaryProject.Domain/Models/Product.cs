using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Product : FullAuditedEntity
    {

        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
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