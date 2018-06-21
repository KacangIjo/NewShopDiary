using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository
{
    public interface IPurchaseRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Purchase>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Purchase> GetAllPurchases();
        ShopDiaryProject.Domain.Models.Purchase GetPurchase(Guid id);
    }
}
