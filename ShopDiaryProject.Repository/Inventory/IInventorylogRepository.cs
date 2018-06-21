using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Inventory
{
    public interface IInventorylogRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Inventorylog>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Inventorylog> GetAllInvetorylog();
        ShopDiaryProject.Domain.Models.Inventorylog GetInventorylog(Guid id);
    }
}
