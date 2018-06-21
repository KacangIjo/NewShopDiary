using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Inventory
{
    public class StorageRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Storage>,IStorageRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Storage> GetAllStorages()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Storage GetStorage(Guid id)
        {
            return GetAllStorages().FirstOrDefault(item => item.Id == id);
        }
    }
}
