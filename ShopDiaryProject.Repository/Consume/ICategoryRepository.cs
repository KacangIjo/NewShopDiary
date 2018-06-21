using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Consume
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Category> GetAllCategories();
        ShopDiaryProject.Domain.Models.Category GetCategory(Guid id);
    }
}
