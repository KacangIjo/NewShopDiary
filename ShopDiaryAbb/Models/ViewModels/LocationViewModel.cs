using ShopDiaryProject.Domain.Models;
using System;

namespace ShopDiaryAbb.Models.ViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string CreatedUserId { get; set; }
        public string AddedUserId { get; set; }
        public bool IsSharedLocation { get; set; }


        public Location ToModel()
        {
            return new Location
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Address = Address,
                Description = Description,
                CreatedUserId = CreatedUserId,
                AddedUserId = AddedUserId
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
            this.AddedUserId = l.AddedUserId;
            this.IsSharedLocation = false;
        }
       
    }
}