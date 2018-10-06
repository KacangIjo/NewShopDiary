using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class ShoplistViewModel:FullAuditedEntity
    {
        public Guid ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Store { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }

        public List<Shopitem> Shopitems { get; set; }
        public ShoplistViewModel()
        {

        }

        public ShoplistViewModel(Shoplist shop)
        {
            Shopitems = new List<Shopitem>();
            if (shop != null)
            {
                ID = shop.Id;
                Name = shop.Name;
                Store = shop.Store;
                Description = shop.Description;
                CreatedUserId = shop.CreatedUserId;
          
            }
        }
        public Shoplist ToModel()
        {
            return new Shoplist
            {
                Name = this.Name,
                Store = this.Store,
                Description = this.Description,
                CreatedUserId=this.CreatedUserId,
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
