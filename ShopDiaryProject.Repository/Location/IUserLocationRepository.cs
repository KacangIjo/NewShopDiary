using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Location
{
    public interface IUserLocationRepository:IGenericRepository<ShopDiaryProject.Domain.Models.UserLocation>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.UserLocation> GetAllUserLocation();
        ShopDiaryProject.Domain.Models.UserLocation GetUserLocation(Guid id);
    }
}
