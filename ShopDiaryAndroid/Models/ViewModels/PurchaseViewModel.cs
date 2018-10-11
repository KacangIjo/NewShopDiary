using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryAndroid.Models.ViewModels
{
    public class PurchaseViewModel
    {
        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Store { get; set; }

        //public Guid InventoryId { get; set; }
        //public Inventory Inventory { get; set; }
        public PurchaseViewModel()
        {

        }
        public Purchase ToModel()
        {
            return new Purchase
            {
                PurchaseDate = this.PurchaseDate,
                Store = this.Store,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public PurchaseViewModel(Purchase p)
        {
            this.PurchaseDate = p.PurchaseDate;
            this.Store = p.Store;
            this.Id = p.Id;
        }
    }
}
