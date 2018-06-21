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
    public class ShopitemController : ApiController
    {
        private ShopitemRepository _shopitemRepository;

        public ShopitemController()
        {
            _shopitemRepository = new ShopitemRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetStorages()
        {
            IEnumerable<ShopitemViewModel> sto = _shopitemRepository.GetAll().ToList().Select(e=>new ShopitemViewModel(e)).ToList();
            return Ok(sto);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(ShopitemViewModel))]
        public IHttpActionResult GetStorage(Guid id)
        {
            ShopitemViewModel storage = new ShopitemViewModel(_shopitemRepository.GetSingle(e => e.Id == id));
            if (storage == null)
            {
                return NotFound();
            }

            return Ok(storage);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStorage(Guid id, ShopitemViewModel storage)
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
                await _shopitemRepository.EditAsync(storage.ToModel());

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
        [ResponseType(typeof(ShopitemViewModel))]
        public IHttpActionResult PostStorage(ShopitemViewModel storage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _shopitemRepository.Add(storage.ToModel());
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
        [ResponseType(typeof(Shopitem))]
        public async Task<IHttpActionResult> DeleteStorage(Guid id)
        {
            Shopitem storage = _shopitemRepository.GetSingle(e => e.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            await _shopitemRepository.DeleteAsync(storage);


            return Ok(storage);
        }



        private bool StorageExist(Guid id)
        {
            IQueryable<Shopitem> sto = _shopitemRepository.GetAll();
            return sto.Count(e => e.Id == id) > 0;
        }
    }
}