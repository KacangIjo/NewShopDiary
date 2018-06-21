using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Inventory
{
    public interface IStorageRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Storage>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Storage> GetAllStorages();
        ShopDiaryProject.Domain.Models.Storage GetStorage(Guid id);
    }
}
