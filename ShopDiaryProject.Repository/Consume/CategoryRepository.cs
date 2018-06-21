using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Consume
{
    public class CategoryRepository:GenericRepository<EF.ShopDiaryDbContext, Category>,ICategoryRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Category> GetAllCategories()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Category GetCategory(Guid id)
        {
            return GetAllCategories().FirstOrDefault(item => item.Id == id);
        }
    }
}
