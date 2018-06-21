
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class ConsumeViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateConsumed { get; set; }
        public int Quantity { get; set; }
        public string AddedUserId { get; set; }
        public Guid InventoryId { get; set; }

        public Consume ToModel()
        {
            return new Consume
            {
                InventoryId = this.InventoryId,
                DateConsumed = this.DateConsumed,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id

            };
        }
        public ConsumeViewModel()
        {

        }

        public ConsumeViewModel(Consume c)
        {
            this.InventoryId = c.InventoryId;
            this.DateConsumed = c.DateConsumed;
            this.Id = c.Id;
        }
       
    }
}