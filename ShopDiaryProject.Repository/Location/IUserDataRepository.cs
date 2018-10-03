using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Location
{
    public interface IUserDataRepository:IGenericRepository<UserData>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.UserData> GetAllUserLocation();
        ShopDiaryProject.Domain.Models.UserData GetUserData(Guid id);
    }
}
