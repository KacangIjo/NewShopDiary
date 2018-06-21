using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Purchase
{
    public class InventoryRepository:GenericRepository<EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Inventory>,IInventoryRepository
    {
    }
}
