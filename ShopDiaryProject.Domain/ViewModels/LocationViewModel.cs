using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class LocationViewModel:FullAuditedEntity
    {
        public Guid ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

        public List<UserLocation> UserLocations { get; set; }
        public List<StorageViewModel> Storages { get; set; }
        public LocationViewModel()
        {

        }
        public LocationViewModel(Location loc)
        {
            Storages = new List<StorageViewModel>();
            if (loc != null)
            {
                ID = loc.Id;
                Name = loc.Name;
                Address = loc.Address;
                Description = loc.Description;
                CreatedUserId = loc.CreatedUserId;
                AddedUserId = loc.AddedUserId;
            }
        }
        public Location ToModel()
        {
            return new Location
            {
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                Description = this.Description,
                Address = this.Address,
                CreatedUserId = this.CreatedUserId,
                AddedUserId = this.AddedUserId,
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
