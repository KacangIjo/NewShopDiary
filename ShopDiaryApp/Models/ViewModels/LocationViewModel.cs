using ShopDiaryProject.Domain.Models;
using System;

namespace ShopDiaryApp.Models.ViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string CreatedUserId { get; set; }

       

        public Location ToModel()
        {
            return new Location
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Address = Address,
                Description = Description,
                CreatedUserId = CreatedUserId
            };
        }
        public LocationViewModel()
        {

        }

        public LocationViewModel(Location l)
        {
            this.Id = l.Id;
            this.Name = l.Name;
            this.Description = l.Description;
            this.Address = l.Address;
            this.CreatedUserId = l.CreatedUserId;
            
        }
       
    }
}