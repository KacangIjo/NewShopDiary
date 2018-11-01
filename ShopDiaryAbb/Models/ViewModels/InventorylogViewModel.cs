using ShopDiaryProject.Domain.Models;
using System;


namespace ShopDiaryAbb.Models.ViewModels
{
    public class InventorylogViewModel
    {
    
        public Guid Id { get; set; }
        public DateTime? LogDate { get; set; }

        public string Description { get; set; }
        public string AddedUserId { get; set; }

        public Guid InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    

        public Inventorylog ToModel()
        {
            return new Inventorylog
            {
                AddedUserId = this.AddedUserId,
                LogDate = this.LogDate,
                InventoryId=this.InventoryId,
                Description=this.Description,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public InventorylogViewModel(Inventorylog p)
        {
            this.AddedUserId = p.AddedUserId;
            this.LogDate = p.LogDate;
            this.InventoryId = p.InventoryId;
            this.Description = p.Description;
            this.Id = p.Id;
        }

        public InventorylogViewModel()
        {

        }
    }
}
