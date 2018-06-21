using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class ShopitemViewModel : FullAuditedEntity
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid ProductID{ get; set; }
        public Guid ShoplistID { get; set; }

        public Shopitem ToModel()
        {
            return new Shopitem
            {
                Quantity = this.Quantity,
                Price = this.Price,
                CreatedDate = this.CreatedDate,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ShopitemViewModel(Shopitem p)
        {
            this.Quantity = p.Quantity;
            this.Price = p.Price;
            this.CreatedDate = p.CreatedDate;
            this.Id = p.Id;
        }

        public ShopitemViewModel()
        {

        }
    }
}
