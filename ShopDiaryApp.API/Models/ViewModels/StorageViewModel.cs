using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class StorageViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string AddedUserId { get; set; }

        public Guid LocationId { get; set; }
       

        public Storage ToModel()
        {
            return new Storage
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Description = Description,
                LocationId = LocationId,
                AddedUserId = AddedUserId,
            };
        }
        public StorageViewModel()
        {

        }

        public StorageViewModel(Storage s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
            this.LocationId = s.LocationId;
            this.AddedUserId = s.AddedUserId;


        }
       
    }
}