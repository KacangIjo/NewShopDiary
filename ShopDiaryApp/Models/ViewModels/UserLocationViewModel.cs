using ShopDiaryProject.Domain.Models;
using System;


namespace ShopDiaryApp.Models.ViewModels
{
    public class UserLocationViewModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public string AddedUserId { get; set; }

        public Guid RoleLocationId { get; set; }
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }

        public UserLocationViewModel()
        {

        }
        public UserLocationViewModel(UserLocation loc)
        {
            Id = loc.Id;
            Description = loc.Description;
            RoleLocationId = loc.RoleLocationId;
            UserId = loc.UserId;
            LocationId = loc.RoleLocationId;
            AddedUserId = loc.AddedUserId;
        }
        public UserLocation ToModel()
        {
            return new UserLocation
            {
                AddedUserId = this.AddedUserId,
                Description = this.Description,
                UserId = this.UserId,
                LocationId=this.LocationId,
                RoleLocationId=this.RoleLocationId,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        #region view model lama
        //[MaxLength(50)]
        //public string Name { get; set; }
        //[MaxLength(150)]
        //public string Address { get; set; }
        //[MaxLength(250)]
        //public string Description { get; set; }

        ////public Guid UserID { get; set; }
        ////public ApplicationUser User { get; set; }

        ////public ICollection<Storage> Storages { get; set; }

        //public Location ToModel()
        //{
        //    return new Location
        //    {
        //        Name = this.Name,
        //        Address=this.Address,
        //        Description = this.Description,
        //        Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
        //    };
        //}

        //public LocationViewModel(Location l)
        //{
        //    this.Name = l.Name;
        //    this.Description = l.Description;
        //    this.Address = l.Address;
        //    this.Id = l.Id;
        //}
        #endregion
    }
}
