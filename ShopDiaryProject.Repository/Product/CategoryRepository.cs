using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Product
{
    public class CategoryRepository:GenericRepository<EF.ShopDiaryDbContext, Category>,ICategoryRepository
    {
        public IEnumerable<Category> GetAllCategory()
        {
            return GetAll();
        }


        public Category GetCategory(Guid id)
        {
            return GetAllCategory().FirstOrDefault(item => item.Id == id);
        }
    }
}
