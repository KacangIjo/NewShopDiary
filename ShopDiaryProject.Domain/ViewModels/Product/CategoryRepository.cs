using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Product
{
    public class CategoryRepository:GenericRepository<ShopDiaryDbContext,Category>,ICategoryRepository
    {
        public Category GetCategoryById(Guid id)
        {
            return GetSingle(id);
        }
    }
}
