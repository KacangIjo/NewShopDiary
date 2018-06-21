using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class PurchaseViewModel:FullAuditedEntity
    {
        public DateTime PurchaseDate { get; set; }
        [MaxLength(250)]
        public string Market { get; set; }

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
                Market = this.Market,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public PurchaseViewModel(Purchase p)
        {
            this.PurchaseDate = p.PurchaseDate;
            this.Market = p.Market;
            this.Id = p.Id;
        }
    }
}
