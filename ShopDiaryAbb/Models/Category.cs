using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class Category : FullAuditedEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        

    }
}