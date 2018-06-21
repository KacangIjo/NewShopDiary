    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class Location : FullAuditedEntity
    {

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }


        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public Location()
        {
            

        }
       
    }
}