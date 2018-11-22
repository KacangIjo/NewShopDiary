using System;

namespace PushNotificationServer.Models.ViewModels
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ItemName { get; set; }
        public bool IsConsumed { get; set; }
        public decimal Price { get; set; }
        public string AddedUserId { get; set; }
        public string CreatedUserId { get; set; }

        public Guid ProductId { get; set; }
        public Guid StorageId { get; set; }


        public Inventory ToModel()
        {
            return new Inventory
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                ExpirationDate = ExpirationDate,
                IsConsumed=IsConsumed,
                Price=Price,
                ProductId=ProductId,
                StorageId=StorageId,
                ItemName=ItemName,
                AddedUserId=AddedUserId,
                CreatedUserId = CreatedUserId
            };
        }
        public InventoryViewModel()
        {

        }

        public InventoryViewModel(Inventory i)
        {
            this.Id = i.Id;
            this.ExpirationDate = i.ExpirationDate;
            this.Price = i.Price;
            this.ProductId = i.ProductId;
            this.StorageId = i.StorageId;
            this.ItemName = i.ItemName;
            this.IsConsumed =i.IsConsumed;
            this.AddedUserId = i.AddedUserId;
            this.CreatedUserId = i.CreatedUserId;


        }
       
    }
}