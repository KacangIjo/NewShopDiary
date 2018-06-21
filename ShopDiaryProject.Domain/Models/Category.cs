using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Category : FullAuditedEntity
    {

        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        

    }
}