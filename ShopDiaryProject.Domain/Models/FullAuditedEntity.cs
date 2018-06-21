using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopDiaryProject.Domain.Models
{
    public class FullAuditedEntity:ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string AddedUserId { get; set; }
        public string CreatedUserId { get; set; }
        public string ModifiedUserId { get; set; }
        public string DeletedUserID { get; set; }
        public bool IsDeleted { get; set; }
        public FullAuditedEntity()
        {
            Id = new Guid();
            CreatedDate = DateTime.Now;
        }

    }
}