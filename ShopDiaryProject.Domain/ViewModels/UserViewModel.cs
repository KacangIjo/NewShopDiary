using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class UserViewModel : FullAuditedEntity
    {

        [MaxLength(200)]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }

        public List<Location> Locations { get; set; }

        public ApplicationUser ToModel()
        {
            return new ApplicationUser
            {
                UserName = this.UserName,
                Email = this.Email,
                Id = this.Id.ToString(),
            };
        }

        public UserViewModel(ApplicationUser c)
        {
            if (c != null)
            {
                this.UserName = c.UserName;
                this.Email = c.Email;
                this.Id = Guid.Parse(c.Id);
            }
        }
        public UserViewModel()
        {

        }

    }
}