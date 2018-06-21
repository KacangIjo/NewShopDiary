using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Consume
{
    public class ConsumeRepository:GenericRepository<EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Consume>,IConsumeRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.Consume> GetAllConsumes()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.Consume GetConsume(Guid id)
        {
            return GetAllConsumes().FirstOrDefault(item => item.Id == id);
        }
    }
}
