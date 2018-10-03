using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Location
{
    public class UserDataRepository : GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.UserData>, IUserDataRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.UserData> GetAllUserLocation()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.UserData GetUserData(Guid id)
        {
            return GetAllUserLocation().FirstOrDefault(item => item.Id == id);
        }


    }
}
