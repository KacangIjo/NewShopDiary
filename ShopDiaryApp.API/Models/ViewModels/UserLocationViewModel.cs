using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class UserLocationViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public string AddedUserId { get; set; }
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }
        public Guid RoleLocationId { get; set; }



        public UserLocation ToModel()
        {
            return new UserLocation
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Description = Description,
                UserId=UserId,
                LocationId=LocationId,
                RoleLocationId=RoleLocationId,
                AddedUserId = AddedUserId,
            };
        }
        public UserLocationViewModel()
        {

        }

        public UserLocationViewModel(UserLocation l)
        {
            this.Id = l.Id;
            this.Description = l.Description;
            this.UserId = l.UserId;
            this.LocationId = l.LocationId;
            this.RoleLocationId = l.RoleLocationId;
            this.AddedUserId = l.AddedUserId;



        }
       
    }
}