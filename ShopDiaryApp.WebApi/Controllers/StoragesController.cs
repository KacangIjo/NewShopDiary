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
using ShopDiaryProject.Repository.Inventory;

namespace ShopDiaryApp.API.Controllers
{
    public class StoragesController : ApiController
    {
        private IStorageRepository _storageRepository;

        public StoragesController()
        {
            _storageRepository = new StorageRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetStorages()
        {
            IQueryable<Storage> sto = _storageRepository.GetAll();
            return Ok(sto);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Storage))]
        public IHttpActionResult GetStorage(Guid id)
        {
            Storage storage = _storageRepository.GetSingle(id);
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStorage(Guid id, Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storage.Id)
            {
                return BadRequest();
            }

            try
            {
                _storageRepository.Edit(storage);

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
        [ResponseType(typeof(Storage))]
        public IHttpActionResult PostStorage(Storage storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _storageRepository.Add(storage);
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
        public IHttpActionResult DeleteStorage(Guid id)
        {
            Storage storage = _storageRepository.GetSingle(id);
            if (storage == null)
            {
                return NotFound();
            }

            _storageRepository.Delete(storage);


            return Ok(storage);
        }



        private bool StorageExist(Guid id)
        {
            IQueryable<Storage> sto = _storageRepository.GetAll();
            return sto.Count(e => e.Id == id) > 0;
        }
    }
}