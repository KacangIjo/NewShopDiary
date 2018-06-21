using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryProject.Repository.Inventory
{
    public class InventorylogRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Inventorylog>,IInventorylogRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Inventorylog> GetAllInvetorylog()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Inventorylog GetInventorylog(Guid id)
        {
            return GetAllInvetorylog().FirstOrDefault(item => item.Id == id);
        }

      
    }
}
