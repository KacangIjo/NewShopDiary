using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class RoleLocationViewModel : FullAuditedEntity
    {


        public int RoleCode { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        //public ICollection<ProductViewModels> Products { get; set; }
        public RoleLocation ToModel()
        {
            return new RoleLocation
            {
                RoleCode = this.RoleCode,
                Description = this.Description,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public RoleLocationViewModel(RoleLocation c)
        {
            this.RoleCode = c.RoleCode;
            this.Description = c.Description;
            this.Id = c.Id;
        }
        public RoleLocationViewModel()
        {

        }

    }
}