using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Product
{
    public interface IShopitemRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Shopitem>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Shopitem> GetAllShopitems();
        ShopDiaryProject.Domain.Models.Shopitem GetShopitem(Guid id);
    }
}
