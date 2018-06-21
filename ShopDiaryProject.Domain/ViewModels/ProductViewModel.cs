using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class ProductViewModel:FullAuditedEntity
    {
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string BarcodeId { get; set; }

        
        //public Guid CategoryId { get; set; }
        //public Category Category { get; set; }
        //public ICollection<Wishlist> Wishlists { get; set; }
        //public ICollection<Inventory> Inventories { get; set; }
        public Product ToModel()
        {
            return new Product
            {
                Name = this.Name,
                BarcodeId = this.BarcodeId,
         
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public ProductViewModel(Product p)
        {
            this.Name = p.Name;
            this.BarcodeId = p.BarcodeId;
            this.Id = p.Id;
        }
        public ProductViewModel()
        {

        }
    }
}
