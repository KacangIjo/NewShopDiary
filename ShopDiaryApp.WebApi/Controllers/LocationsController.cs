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
using ShopDiaryProject.Repository.Storage;

namespace ShopDiaryApp.API.Controllers
{
    public class LocationsController : ApiController
    {
        private ILocationRepository _locationRepository;

        public LocationsController()
        {
            _locationRepository = new LocationRepository();
        }

        // GET: api/Categories
        public IHttpActionResult getLocations()
        {
            IQueryable<Location> loc = _locationRepository.GetAll();
            return Ok(loc);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(Guid id)
        {
            Location location = _locationRepository.GetSingle(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(Guid id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            try
            {
                _locationRepository.Edit(location);

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

        // POST: api/Categories
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _locationRepository.Add(location);
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
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteCategory(Guid id)
        {
            Location location = _locationRepository.GetSingle(id);
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
}