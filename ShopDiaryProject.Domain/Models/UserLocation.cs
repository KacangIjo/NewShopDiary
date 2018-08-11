    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.Models
{
    public class UserLocation : FullAuditedEntity
    {
        public Guid RegisteredUser { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

        public Guid RoleLocationId { get; set; }
        public RoleLocation RoleLocation { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public UserLocation()
        {
            

        }
       
    }
}