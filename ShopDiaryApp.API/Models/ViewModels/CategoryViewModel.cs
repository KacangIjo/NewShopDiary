
using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryApp.API.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public Guid UserId { get; set; }

        public Category ToModel()
        {
            return new Category
            {
                Id = (Id == Guid.Empty) ? Guid.NewGuid() : Id,
                Name = Name,
                Description = Description,
                UserId=UserId,

            };
        }
        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
            this.Description = c.Description;
            this.UserId = c.UserId;
        }

    }
}