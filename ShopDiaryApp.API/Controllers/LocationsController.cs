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

namespace ShopDiaryApp.API.Controllers
{

    #region LocationController
    public class LocationsController : ApiController
    {
        private LocationRepository _locationRepository;

        public LocationsController()
        {
            _locationRepository = new LocationRepository();
        }

        // GET: api/Locations
        public IHttpActionResult getLocations()
        {
            IEnumerable<LocationViewModel> loc = _locationRepository.GetAll().ToList().Select(e=>new LocationViewModel(e)).ToList();
            return Ok(loc);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(LocationViewModel))]
        public IHttpActionResult GetLocation(Guid id)
        {
            LocationViewModel location = new LocationViewModel(_locationRepository.GetSingle(e => e.Id == id));
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Categories/5
        [HttpPut]
        [ResponseType(typeof(LocationViewModel))]
        public async Task<IHttpActionResult> PutLocation(Guid id, LocationViewModel location)
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
                await _locationRepository.EditAsync(location.ToModel());
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
        [ResponseType(typeof(LocationViewModel))]
        public IHttpActionResult PostLocation(LocationViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _locationRepository.Add(location.ToModel());
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
        //    Location location = _locationRepository.GetSingle(e => e.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    await _locationRepository.DeleteAsync(location);


        //    return Ok(location);
        //}
        public IHttpActionResult DeleteLocation(Guid id)
        {
            Location location = _locationRepository.GetSingle(e => e.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _locationRepository.Delete(location);


            return Ok(location);
        }



        private bool LocationExist(Guid id)
        {
            IQueryable<Location> loc = _locationRepository.GetAll();
            return loc.Count(e => e.Id == id) > 0;
        }
    }
    #endregion
}