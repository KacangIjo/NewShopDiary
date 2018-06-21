using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class ShoplistViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Market { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public string AddedUserId { get; set; }


       

        public Shoplist ToModel()
        {
            return new Shoplist
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Description = Description

            };
        }
        public ShoplistViewModel()
        {

        }

        public ShoplistViewModel(Shoplist s)
        {
            this.Id = s.Id;
            this.Name = s.Name;
            this.Description = s.Description;
            
        }
       
    }
}