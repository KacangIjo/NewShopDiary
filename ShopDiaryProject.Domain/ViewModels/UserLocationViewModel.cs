using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class UserLocationViewModel : FullAuditedEntity
    {
        public Guid ID { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public Guid RoleLocationId { get; set; }
        public Guid UserId { get; set; }
        public Guid LocationId { get; set; }

        public UserLocationViewModel()
        {

        }
        public UserLocationViewModel(UserLocation loc)
        {
            if (loc != null)
            {
                ID = loc.Id;
                Description = loc.Description;
                RoleLocationId = loc.RoleLocationId;
                UserId = loc.UserId;
                LocationId = loc.RoleLocationId;
                CreatedUserId = loc.CreatedUserId;
                IsDeleted = loc.IsDeleted;
            }
        }
        public UserLocation ToModel()
        {
            return new UserLocation
            {

                IsDeleted = this.IsDeleted,
                CreatedUserId = this.CreatedUserId,
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
