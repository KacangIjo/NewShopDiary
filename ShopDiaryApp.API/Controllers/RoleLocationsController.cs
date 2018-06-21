using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;
using ShopDiaryProject.Repository.Storage;
using ShopDiaryApp.API.Models.ViewModels;
using System.Threading.Tasks;
using ShopDiaryProject.Repository.Location;

namespace ShopDiaryApp.API.Controllers
{

    #region LocationController
    public class RoleLocationsController : ApiController
    {
        private RoleLocationRepository _roleLocationRepository;

        public RoleLocationsController()
        {
            _roleLocationRepository = new RoleLocationRepository();
        }

        // GET: api/Locations
        public IHttpActionResult getLocations()
        {
            IEnumerable<RoleLocationViewModel> loc = _roleLocationRepository.GetAll().ToList().Select(e=>new RoleLocationViewModel(e)).ToList();
            return Ok(loc);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(RoleLocationViewModel))]
        public IHttpActionResult GetLocation(Guid id)
        {
            RoleLocationViewModel location = new RoleLocationViewModel(_roleLocationRepository.GetSingle(e => e.Id == id));
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Categories/5
        [HttpPut]
        [ResponseType(typeof(RoleLocationViewModel))]
        public async Task<IHttpActionResult> PutLocation(Guid id, RoleLocationViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            location.Id = id;

            try
            {
                await _roleLocationRepository.EditAsync(location.ToModel());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Locations
        [ResponseType(typeof(RoleLocationViewModel))]
        public IHttpActionResult PostLocation(RoleLocationViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _roleLocationRepository.Add(location.ToModel());
            }
            catch (DbUpdateException)
            {
                if (LocationExist(location.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/Categories/5
        [HttpDelete]
        [ResponseType(typeof(Location))]

        //public async Task<IHttpActionResult> DeleteLocation(Guid id)
        //{
        //    Location location = _roleLocationRepository.GetSingle(e => e.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    await _roleLocationRepository.DeleteAsync(location);


        //    return Ok(location);
        //}
        public IHttpActionResult DeleteLocation(Guid id)
        {
            RoleLocation location = _roleLocationRepository.GetSingle(e => e.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _roleLocationRepository.Delete(location);


            return Ok(location);
        }



        private bool LocationExist(Guid id)
        {
            IQueryable<RoleLocation> loc = _roleLocationRepository.GetAll();
            return loc.Count(e => e.Id == id) > 0;
        }
    }
    #endregion
}