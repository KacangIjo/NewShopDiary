using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Storage
{
    public interface ILocationRepository:IGenericRepository<ShopDiaryProject.Domain.Models.Location>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.Location> GetAllLocations();
        ShopDiaryProject.Domain.Models.Location GetLocation(Guid id);
    }
}
