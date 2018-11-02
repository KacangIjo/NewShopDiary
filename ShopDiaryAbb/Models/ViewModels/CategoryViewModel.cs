

using System;

namespace ShopDiaryAbb.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string CreatedUserId { get; set; }
        public string AddedUserId { get; set; }


        public Category ToModel()
        {
            return new Category
            {
                Id = (Id==Guid.Empty)?Guid.NewGuid():Id,
                Name = Name,
                Description = Description,
                UserId = Guid.Parse(UserId),
                AddedUserId =AddedUserId,
                CreatedUserId=CreatedUserId
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
            this.UserId = c.UserId.ToString();
            this.AddedUserId = c.AddedUserId;
            this.CreatedUserId = c.CreatedUserId;
        }
       
    }
}