using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDiaryProject.Domain.Models;

namespace ShopDiaryProject.Repository.Location
{
    public class RoleLocationRepository:GenericRepository<ShopDiaryProject.EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.RoleLocation>,IRoleLocationRepository
    {
        public IEnumerable<ShopDiaryProject.Domain.Models.RoleLocation> GetAllRoleLocation()
        {
            return GetAll();
        }


        public ShopDiaryProject.Domain.Models.RoleLocation GetRoleLocation(Guid id)
        {
            return GetAllRoleLocation().FirstOrDefault(item => item.Id == id);
        }

      
    }
}
