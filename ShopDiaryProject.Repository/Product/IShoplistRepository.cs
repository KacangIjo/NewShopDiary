using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Product
{
    public interface IShoplistRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Shoplist>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Shoplist> GetAllShoplists();
        ShopDiaryProject.Domain.Models.Shoplist GetShoplist(Guid id);
    }
}
