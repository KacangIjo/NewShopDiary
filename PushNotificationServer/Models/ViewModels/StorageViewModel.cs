using System;

namespace PushNotificationServer.Models.ViewModels
{
    public class StorageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string AddedUserId { get; set; }
        public string CreatedUserId { get; set; }


        public Guid LocationId { get; set; }

        public StorageViewModel()
        {

        }

        public StorageViewModel(Storage s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
            this.Area = s.Area;
            this.AddedUserId = s.AddedUserId;
            this.CreatedUserId = s.CreatedUserId;
            this.LocationId = s.LocationId;

        }



        public Storage ToModel()
        {
            return new Storage
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Area = Area,
                Description = Description,
                AddedUserId= AddedUserId,
                CreatedUserId = CreatedUserId,
                LocationId = LocationId
            };
        }
      

        
       
    }
}