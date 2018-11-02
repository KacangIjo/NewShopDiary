    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopDiaryAbb.Models
{
    public class UserData : FullAuditedEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public UserData()
        {
            

        }
       
    }
}