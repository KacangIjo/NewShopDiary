    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class UserLocation : FullAuditedEntity
    {
        public Guid RegisteredUser { get; set; }

        public int Create { get; set; }
        public int Read { get; set; }
        public int Update { get; set; }
        public int Delete { get; set; }
        
        public string Description { get; set; }
        
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
      
       
        

        public UserLocation()
        {
            

        }
       
    }
}