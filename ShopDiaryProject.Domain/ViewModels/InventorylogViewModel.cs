using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class InventorylogViewModel : FullAuditedEntity
    {
    
        public DateTime? LogDate { get; set; }

        public string Description { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public Inventorylog ToModel()
        {
            return new Inventorylog
            {
                CreatedUserId = this.CreatedUserId,
                LogDate = this.LogDate,
                CreatedDate = this.CreatedDate,
                InventoryId=this.InventoryId,
                Description=this.Description,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public InventorylogViewModel(Inventorylog p)
        {
            this.CreatedUserId = p.CreatedUserId;
            this.LogDate = p.LogDate;
            this.CreatedDate = p.CreatedDate;
            this.InventoryId = p.InventoryId;
            this.Description = p.Description;
            this.Id = p.Id;
        }

        public InventorylogViewModel()
        {

        }
    }
}
