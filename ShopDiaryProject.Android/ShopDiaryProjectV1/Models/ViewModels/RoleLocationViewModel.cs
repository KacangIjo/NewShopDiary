using ShopDiaryProject.Domain.Models;
using System;


namespace ShopDiaryProject.Android.Models.ViewModels
{
    public class RoleLocationViewModel 
    {

        public Guid Id { get; set; }
        public int RoleCode { get; set; }
        public string Description { get; set; }

        //public ICollection<ProductViewModels> Products { get; set; }
        public RoleLocation ToModel()
        {
            return new RoleLocation
            {
                RoleCode = this.RoleCode,
                Description = this.Description,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public RoleLocationViewModel(RoleLocation c)
        {
            this.RoleCode = c.RoleCode;
            this.Description = c.Description;
            this.Id = c.Id;
        }
        public RoleLocationViewModel()
        {

        }

    }
}