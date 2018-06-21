using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Purchase
{
    public interface IInventoryRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Inventory>
    {
    }
}
