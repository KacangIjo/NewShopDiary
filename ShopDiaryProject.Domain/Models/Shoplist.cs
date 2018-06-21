using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Shoplist : FullAuditedEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Market { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        
        public virtual ICollection<Shopitem> ShopItems { get; set; }
        public Shoplist()
        {
           

        }
    }
}