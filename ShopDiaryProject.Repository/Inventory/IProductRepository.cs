using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Inventory
{
    public interface IProductRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Product>
    {
    }
}
