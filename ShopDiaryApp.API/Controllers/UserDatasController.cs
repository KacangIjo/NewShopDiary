using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ShopDiaryProject.Domain.Models;
using ShopDiaryApp.API.Models.ViewModels;
using System.Threading.Tasks;
using ShopDiaryProject.Repository.Location;

namespace ShopDiaryApp.API.Controllers
{
    public class UserDatasController : ApiController
    {
        private UserDataRepository _userDataRepository;

        public UserDatasController()
        {
            _userDataRepository = new UserDataRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetUserDatas()
        {
            IEnumerable<UserDataViewModel> sto = _userDataRepository.GetAll().ToList().Select(e=>new UserDataViewModel(e)).ToList();
            return Ok(sto);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(UserDataViewModel))]
        public IHttpActionResult GetUserData(Guid id)
        {
            UserDataViewModel storage = new UserDataViewModel(_userDataRepository.GetSingle(e => e.Id == id));
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserData(Guid id, UserDataViewModel storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storage.Id)
            {
                return BadRequest();
            }
            storage.Id = id;
            try
            {
                await _userDataRepository.EditAsync(storage.ToModel());

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExist(id))
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
        [ResponseType(typeof(UserDataViewModel))]
        public IHttpActionResult PostUserData(UserDataViewModel storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _userDataRepository.Add(storage.ToModel());
            }
            catch (DbUpdateException)
            {
                if (UserDataExist(storage.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = storage.Id }, storage);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(UserData))]
        public async Task<IHttpActionResult> DeleteUserData(Guid id)
        {
            UserData userdata = _userDataRepository.GetSingle(e => e.Id == id);
            if (userdata == null)
            {
                return NotFound();
            }

            await _userDataRepository.DeleteAsync(userdata);


            return Ok(userdata);
        }



        private bool UserDataExist(Guid id)
        {
            IQueryable<UserData> sto = _userDataRepository.GetAll();
            return sto.Count(e => e.Id == id) > 0;
        }
    }
}