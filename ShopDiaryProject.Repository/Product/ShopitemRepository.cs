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
    public class ShopitemRepository : GenericRepository<EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Shopitem>, IShopitemRepository
    {
        public IEnumerable<Domain.Models.Shopitem> GetAllShopitems()
        {
            return GetAll();
        }

        public Domain.Models.Shopitem GetShopitem(Guid id)
        {
            return GetAllShopitems().FirstOrDefault(item => item.Id == id);
        }
    }
}
