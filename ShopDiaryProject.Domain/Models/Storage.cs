using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Storage : FullAuditedEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Area { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public Storage()
        {
           

        }
    }
}