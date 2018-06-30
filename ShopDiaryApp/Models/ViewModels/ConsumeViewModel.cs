using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryApp.Models.ViewModels
{
    public class ConsumeViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateConsumed { get; set; }
        public Guid InventoryId { get; set; }
        //public Inventory Inventory { get; set; }

        public Consume ToModel()
        {
            return new Consume
            {
                InventoryId = this.InventoryId,
                DateConsumed = this.DateConsumed,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ConsumeViewModel(Consume c)
        {
            this.InventoryId = c.InventoryId;
            this.DateConsumed = c.DateConsumed;
            this.Id = c.Id;
        }
    }
}