using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class InventoryViewModel:FullAuditedEntity
    {
        public DateTime ExpirationDate { get; set; }
        public string ItemName { get; set; }
        public bool IsConsumed { get; set; }
        public decimal Price { get; set; }


        public Guid ProductId { get; set; }
        public Guid StorageId { get; set; }
  
        public Inventory ToModel()
        {
            return new Inventory
            {
                ItemName=this.ItemName,
                IsConsumed=this.IsConsumed,
                IsDeleted=this.IsDeleted,
                StorageId=this.StorageId,
                Price=this.Price,
                ExpirationDate = this.ExpirationDate,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }
        

        public InventoryViewModel(Inventory i)
        {
            if (i != null)
            {
                this.ExpirationDate = i.ExpirationDate;
                this.Id = i.Id;
                this.ItemName = i.ItemName;
                this.StorageId = i.StorageId;
                this.IsDeleted = i.IsDeleted;
                this.IsConsumed = i.IsConsumed;
                this.Price = i.Price;
               
            }
        }
        public InventoryViewModel()
        {

        }

    }
}
