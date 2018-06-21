using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopDiaryProject.Repository.Inventory
{
    public class ProductRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Product>,IProductRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Product> GetAllProduct()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Product GetProduct(Guid id)
        {
            return GetAllProduct().FirstOrDefault(item => item.Id == id);
        }
    }
}
