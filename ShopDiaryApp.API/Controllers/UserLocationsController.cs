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
    public class UserLocationsController : ApiController
    {
        private UserLocationRepository _userLocationRepository;

        public UserLocationsController()
        {
            _userLocationRepository = new UserLocationRepository();
        }

        // GET: api/Locations
        public IHttpActionResult getLocations()
        {
            IEnumerable<UserLocationViewModel> loc = _userLocationRepository.GetAll().ToList().Select(e=>new UserLocationViewModel(e)).ToList();
            return Ok(loc);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(UserLocationViewModel))]
        public IHttpActionResult GetLocation(Guid id)
        {
            UserLocationViewModel location = new UserLocationViewModel(_userLocationRepository.GetSingle(e => e.Id == id));
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Categories/5
        [HttpPut]
        [ResponseType(typeof(UserLocationViewModel))]
        public async Task<IHttpActionResult> PutLocation(Guid id, UserLocationViewModel location)
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
                await _userLocationRepository.EditAsync(location.ToModel());
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
        [ResponseType(typeof(UserLocationViewModel))]
        public IHttpActionResult PostLocation(UserLocationViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _userLocationRepository.Add(location.ToModel());
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
        [ResponseType(typeof(UserLocation))]

        //public async Task<IHttpActionResult> DeleteLocation(Guid id)
        //{
        //    UserLocation location = _userLocationRepository.GetSingle(e => e.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    await _userLocationRepository.DeleteAsync(location);


        //    return Ok(location);
        //}
        public IHttpActionResult DeleteLocation(Guid id)
        {
            UserLocation location = _userLocationRepository.GetSingle(e => e.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _userLocationRepository.Delete(location);


            return Ok(location);
        }



        private bool LocationExist(Guid id)
        {
            IQueryable<UserLocation> loc = _userLocationRepository.GetAll();
            return loc.Count(e => e.Id == id) > 0;
        }
    }
    #endregion
}