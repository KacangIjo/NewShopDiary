using System;

namespace ShopDiaryAbb.Models.ViewModels
{
    public class ShopitemViewModel 
    {
        public Guid Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid ProductID{ get; set; }
        public Guid ShoplistID { get; set; }

        public Shopitem ToModel()
        {
            return new Shopitem
            {
                ItemName=this.ItemName,
                Quantity = this.Quantity,
                Price = this.Price,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ShopitemViewModel(Shopitem p)
        {
            this.ItemName = p.ItemName;
            this.Quantity = p.Quantity;
            this.Price = p.Price;
            this.Id = p.Id;
        }

        public ShopitemViewModel()
        {

        }
    }
}
