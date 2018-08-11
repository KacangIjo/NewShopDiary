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
        public Guid RegisteredUser { get; set; }

        public int Create { get; set; }
        public int Read { get; set; }
        public int Update { get; set; }
        public int Delete { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public Guid RoleLocationId { get; set; }
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
                Create = loc.Create;
                Read = loc.Read;
                Update = loc.Update;
                Delete = loc.Delete;
                RegisteredUser = loc.RegisteredUser;
                LocationId = loc.LocationId;
                CreatedUserId = loc.CreatedUserId;
                IsDeleted = loc.IsDeleted;
            }
        }
        public UserLocation ToModel()
        {
            return new UserLocation
            {
                Create = this.Create,
                Read = this.Read,
                Update = this.Update,
                Delete = this.Delete,
                IsDeleted = this.IsDeleted,
                CreatedUserId = this.CreatedUserId,
                Description = this.Description,
                RegisteredUser = this.RegisteredUser,
                LocationId = this.LocationId,

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
