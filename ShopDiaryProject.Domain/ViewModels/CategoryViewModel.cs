using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDiaryProject.Domain.ViewModels
{
    public class CategoryViewModel : FullAuditedEntity
    {

        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        //public ICollection<ProductViewModels> Products { get; set; }
        public Guid UserId { get; set; }
        public Category ToModel()
        {
            return new Category
            {
                Name = this.Name,
                Description = this.Description,
                UserId=this.UserId,
                CreatedUserId=CreatedUserId,
                Id = this.Id == Guid.Empty ? Guid.NewGuid() : this.Id
            };
        }

        public CategoryViewModel(Category c)
        {
            this.Name = c.Name;
            this.Description = c.Description;
            this.Id = c.Id;
        }
        public CategoryViewModel()
        {

        }

    }
}