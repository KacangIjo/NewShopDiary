using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class RoleLocation : FullAuditedEntity
    {

        public int RoleCode { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        public virtual ICollection<UserLocation> UserLocations { get; set; }
        public RoleLocation()
        {

        }
        

        

    }
}