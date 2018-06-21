using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class StorageViewModel:FullAuditedEntity
    {
        public Guid ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string Area { get; set; }


        public Guid LocationID { get; set; }
        public List<InventoryViewModel> Inventories { get; set; }
        public StorageViewModel()
        {

        }

        public StorageViewModel(Storage store)
        {
            Inventories = new List<InventoryViewModel>();
            if (store !=null)
            {
                ID = store.Id;
                Area = store.Area;
                Description = store.Description;
                LocationID = store.LocationId;
            }
        }
    
        public Storage ToModel()
        {
            return new Storage
            {
                Name = this.Name,
                IsDeleted = this.IsDeleted,
                Description = this.Description,
                Area = this.Area,
                LocationId = this.LocationID,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        #region view model lama
        //[MaxLength(50)]
        //public string Name { get; set; }
        //[MaxLength(200)]
        //public string Description { get; set; }
        //[MaxLength(200)]
        //public string Area { get; set; }
        //[MaxLength(200)]
        //public string Block { get; set; }

        ////public Guid LocationId { get; set; }
        ////public Location Location { get; set; }

        ////public ICollection<Inventory> Inventories { get; set; }

        //public Storage ToModel()
        //{
        //    return new Storage
        //    {
        //        Name = this.Name,
        //        Description = this.Description,
        //        Area = this.Area,
        //        Block=this.Block,
        //        Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
        //    };
        //}

        //public StorageViewModel(Storage s)
        //{
        //    this.Name = s.Name;
        //    this.Description = s.Description;
        //    this.Area = s.Area;
        //    this.Block = s.Block;
        //    this.Id = s.Id;
        //}
        #endregion
    }
}
