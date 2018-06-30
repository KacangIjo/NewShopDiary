
using ShopDiaryProject.Domain.Models;
using System;

namespace ShopDiaryApp.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        
       

        public Category ToModel()
        {
            return new Category
            {
                Id = (Id==Guid.Empty)?Guid.NewGuid():Id,
                Name = Name,
                Description = Description,
                
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
        }
       
    }
}