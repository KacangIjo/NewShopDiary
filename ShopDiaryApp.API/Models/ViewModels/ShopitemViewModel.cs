using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class ShopitemViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string AddedUserId { get; set; }

        public Guid ProductId { get; set; }
        public Guid ShoplistId { get; set; }

        public Shopitem ToModel()
        {
            return new Shopitem
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Quantity=Quantity,
                Price = Price,
                ProductId=ProductId,
                ShoplistId=ShoplistId,
                AddedUserId = AddedUserId
            };
        }

        public ShopitemViewModel(Shopitem s)
        {
            this.Id = s.Id;
            this.Quantity = s.Quantity;
            this.Price = s.Price;
            this.ProductId = s.ProductId;
            this.ShoplistId = s.ShoplistId;
            this.AddedUserId = s.AddedUserId;
            
        }
        public ShopitemViewModel()
        {

        }

    }
}