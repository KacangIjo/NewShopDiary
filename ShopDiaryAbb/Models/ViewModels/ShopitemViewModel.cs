using System;

namespace ShopDiaryAbb.Models.ViewModels
{
    public class ShopitemViewModel 
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string AddedUserId
        {
            get; set;
        }
        public Guid ProductID{ get; set; }
        public Guid ShoplistId{get; set; }

        public Shopitem ToModel()
        {
            return new Shopitem
            {
                ItemName = this.ItemName,
                Quantity = this.Quantity,
                Price = this.Price,
                AddedUserId = this.AddedUserId,
                ShoplistId = this.ShoplistId,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ShopitemViewModel(Shopitem p)
        {
            this.ItemName = p.ItemName;
            this.Quantity = p.Quantity;
            this.AddedUserId = p.AddedUserId;
            this.ShoplistId = p.ShoplistId;
            this.Price = p.Price;
            this.Id = p.Id;
        }

        public ShopitemViewModel()
        {

        }
    }
}
