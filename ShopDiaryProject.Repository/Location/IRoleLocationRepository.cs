using ShopDiaryProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Location
{
    public interface IRoleLocationRepository:IGenericRepository<ShopDiaryProject.Domain.Models.RoleLocation>
    {
        IEnumerable<ShopDiaryProject.Domain.Models.RoleLocation> GetAllRoleLocation();
        ShopDiaryProject.Domain.Models.RoleLocation GetRoleLocation(Guid id);
    }
}
