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
using ShopDiaryProject.Repository.Product;

namespace ShopDiaryApp.API.Controllers
{
    public class ShoplistController : ApiController
    {
        private ShoplistRepository _shoplistRepository;

        public ShoplistController()
        {
            _shoplistRepository = new ShoplistRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetStorages()
        {
            IEnumerable<ShoplistViewModel> sto = _shoplistRepository.GetAll().ToList().Select(e=>new ShoplistViewModel(e)).ToList();
            return Ok(sto);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(ShoplistViewModel))]
        public IHttpActionResult GetStorage(Guid id)
        {
            ShoplistViewModel storage = new ShoplistViewModel(_shoplistRepository.GetSingle(e => e.Id == id));
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorage(Guid id, ShoplistViewModel storage)
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
                await _shoplistRepository.EditAsync(storage.ToModel());

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
        [ResponseType(typeof(ShoplistViewModel))]
        public IHttpActionResult PostStorage(ShoplistViewModel storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _shoplistRepository.Add(storage.ToModel());
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
        [ResponseType(typeof(Shoplist))]
        public async Task<IHttpActionResult> DeleteStorage(Guid id)
        {
            Shoplist storage = _shoplistRepository.GetSingle(e => e.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            await _shoplistRepository.DeleteAsync(storage);


            return Ok(storage);
        }



        private bool StorageExist(Guid id)
        {
            IQueryable<Shoplist> sto = _shoplistRepository.GetAll();
            return sto.Count(e => e.Id == id) > 0;
        }
    }
}