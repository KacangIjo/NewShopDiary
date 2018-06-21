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
using ShopDiaryProject.Repository.Inventory;
using ShopDiaryApp.API.Models.ViewModels;
using System.Threading.Tasks;

namespace ShopDiaryApp.API.Controllers
{
    public class StoragesController : ApiController
    {
        private StorageRepository _storageRepository;

        public StoragesController()
        {
            _storageRepository = new StorageRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetStorages()
        {
            IEnumerable<StorageViewModel> sto = _storageRepository.GetAll().ToList().Select(e=>new StorageViewModel(e)).ToList();
            return Ok(sto);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(StorageViewModel))]
        public IHttpActionResult GetStorage(Guid id)
        {
            StorageViewModel storage = new StorageViewModel(_storageRepository.GetSingle(e => e.Id == id));
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorage(Guid id, StorageViewModel storage)
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
                await _storageRepository.EditAsync(storage.ToModel());

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageExist(id))
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
        [ResponseType(typeof(StorageViewModel))]
        public IHttpActionResult PostStorage(StorageViewModel storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _storageRepository.Add(storage.ToModel());
            }
            catch (DbUpdateException)
            {
                if (StorageExist(storage.Id))
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
        [ResponseType(typeof(Storage))]
        public async Task<IHttpActionResult> DeleteStorage(Guid id)
        {
            Storage storage = _storageRepository.GetSingle(e => e.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            await _storageRepository.DeleteAsync(storage);


            return Ok(storage);
        }



        private bool StorageExist(Guid id)
        {
            IQueryable<Storage> sto = _storageRepository.GetAll();
            return sto.Count(e => e.Id == id) > 0;
        }
    }
}