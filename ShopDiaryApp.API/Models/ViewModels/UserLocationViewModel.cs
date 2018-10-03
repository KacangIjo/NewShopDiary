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
        public Guid RegisteredUser { get; set; }
        public string Description { get; set; }
        public int Create { get; set; }
        public int Read { get; set; }
        public int Update { get; set; }
        public int Delete { get; set; }

        public string AddedUserId { get; set; }
        public Guid LocationId { get; set; }



        public UserLocation ToModel()
        {
            return new UserLocation
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Description = Description,
                RegisteredUser = RegisteredUser,
                LocationId=LocationId,
                Create = Create,
                Read = Read,
                Update = Update,
                Delete = Delete,
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
            this.RegisteredUser = l.RegisteredUser;
            this.LocationId = l.LocationId;
            this.Create = l.Create;
            this.Read = l.Read;
            this.Update = l.Update;
            this.Delete = l.Delete;
            this.AddedUserId = l.AddedUserId;



        }
       
    }
}