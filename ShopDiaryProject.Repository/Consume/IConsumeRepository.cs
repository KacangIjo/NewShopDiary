using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Consume
{
    public interface IConsumeRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Consume>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Consume> GetAllConsumes();
        ShopDiaryProject.Domain.Models.Consume GetConsume(Guid id);
    }
}
