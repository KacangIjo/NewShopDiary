using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository
{
    public class PurchaseRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Purchase>,IPurchaseRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Purchase> GetAllPurchases()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Purchase GetPurchase(Guid id)
        {
            return GetAllPurchases().FirstOrDefault(item => item.Id == id);
        }
    }
}
