using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Product
{
    public class ShoplistRepository : GenericRepository<EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Shoplist>, IShoplistRepository
    {
        public IEnumerable<Domain.Models.Shoplist> GetAllShoplists()
        {
            return GetAll();
        }

        public Domain.Models.Shoplist GetShoplist(Guid id)
        {
            return GetAllShoplists().FirstOrDefault(item => item.Id == id);
        }
    }
}
