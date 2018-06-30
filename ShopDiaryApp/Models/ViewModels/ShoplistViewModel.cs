using ShopDiaryProject.Domain.Models;
using System;

namespace ShopDiaryApp.Models.ViewModels
{
    public class ShoplistViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Market { get; set; }
        public string Description { get; set; }
        public string AddedUserId { get; set; }

        public ShoplistViewModel()
        {

        }

        public ShoplistViewModel(Shoplist shop)
        {

            if (shop != null)
            {
                Id = shop.Id;
                Name = shop.Name;
                Market = shop.Market;
                AddedUserId = shop.AddedUserId;
                Description = shop.Description;
          
            }
        }
        public Shoplist ToModel()
        {
            return new Shoplist
            {
                Name = this.Name,
                Market = this.Market,
                Description = this.Description,
                AddedUserId = this.AddedUserId,
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
