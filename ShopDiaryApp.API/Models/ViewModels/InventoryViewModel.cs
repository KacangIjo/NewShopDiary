
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class InventoryViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsConsumed { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ItemName { get; set; }
        public Guid ProductId { get; set; }
        public Guid StorageId { get; set; }
        public string AddedUserId { get; set; }

        public Inventory ToModel()
        {
            return new Inventory
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                ExpirationDate = ExpirationDate,
                Price = Price,
                Stock=Stock,
                ProductId=ProductId,
                StorageId=StorageId,
                ItemName=ItemName,
                IsDeleted=IsDeleted,
                IsConsumed=IsConsumed,
                AddedUserId = AddedUserId,
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
            this.Stock = i.Stock;
            this.ProductId = i.ProductId;
            this.StorageId = i.StorageId;
            this.ItemName = i.ItemName;
            this.AddedUserId = i.AddedUserId;
        }
       
    }
}