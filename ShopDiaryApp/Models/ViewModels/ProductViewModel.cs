using ShopDiaryProject.Domain.Models;
using System;

namespace ShopDiaryApp.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BarcodeId { get; set; }

        public Guid CategoryId { get; set; }


        public Product ToModel()
        {
            return new Product
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                BarcodeId = BarcodeId,
                CategoryId = CategoryId
               
            };
        }
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product p)
        {
            this.Id = p.Id;
            this.Name = p.Name;
            this.BarcodeId = p.BarcodeId;
            this.CategoryId = p.CategoryId;


        }
       
    }
}