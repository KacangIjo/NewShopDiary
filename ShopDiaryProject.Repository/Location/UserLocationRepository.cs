using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryProject.Repository.Location
{
    public class UserLocationRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.UserLocation>,IUserLocationRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.UserLocation> GetAllUserLocation()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.UserLocation GetUserLocation(Guid id)
        {
            return GetAllUserLocation().FirstOrDefault(item => item.Id == id);
        }

      
    }
}
