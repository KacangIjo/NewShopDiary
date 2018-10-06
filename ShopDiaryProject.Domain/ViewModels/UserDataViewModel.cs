using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class UserDataViewModel : FullAuditedEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }


        public UserDataViewModel()
        {

        }
        public UserDataViewModel(UserData loc)
        {
            if (loc != null)
            {
                Id = loc.Id;
                UserId = loc.UserId;
                Email = loc.Email;
            }
        }
        public UserData ToModel()
        {
            return new UserData
            {
                UserId = this.UserId,
                Email = this.Email,
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
