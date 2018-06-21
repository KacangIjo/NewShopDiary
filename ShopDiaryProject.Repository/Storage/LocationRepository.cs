using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDiaryProject.Repository.Storage
{
    public class LocationRepository : GenericRepository<EF.ShopDiaryDbContext, ShopDiaryProject.Domain.Models.Location>, ILocationRepository
    {
        public IEnumerable<Domain.Models.Location> GetAllLocations()
        {
            return GetAll();
        }

        public Domain.Models.Location GetLocation(Guid id)
        {
            return GetAllLocations().FirstOrDefault(item => item.Id == id);
        }
    }
}
