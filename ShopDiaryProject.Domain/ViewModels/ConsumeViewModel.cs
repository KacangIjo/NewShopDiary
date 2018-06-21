using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class ConsumeViewModel : FullAuditedEntity
    {
        public DateTime DateConsumed { get; set; }
        public int Quantity { get; set; }
        public bool IsConsumed { get; set; }
        public Guid InventoryId { get; set; }
        //public Inventory Inventory { get; set; }

        public Consume ToModel()
        {
            return new Consume
            {
                InventoryId=this.InventoryId,
                DateConsumed = this.DateConsumed,
                IsConsumed=this.IsConsumed,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ConsumeViewModel(Consume c)
        {
            this.InventoryId = c.InventoryId;
            this.DateConsumed = c.DateConsumed;
            this.IsConsumed = c.IsConsumed;
            this.Id = c.Id;
        }
        public ConsumeViewModel()
        {

        }
    }
}
