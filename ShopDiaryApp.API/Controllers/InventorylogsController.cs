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
using ShopDiaryProject.Repository.Purchase;
using ShopDiaryApp.API.Models.ViewModels;
using System.Threading.Tasks;
using ShopDiaryProject.Repository.Inventory;

namespace ShopDiaryApp.API.Controllers
{
    public class InventorylogsController : ApiController
    {
        private InventorylogRepository _inventorylogRepository;

        public InventorylogsController()
        {
            _inventorylogRepository = new InventorylogRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetInventories()
        {
            IEnumerable<InventorylogViewModel> inv = _inventorylogRepository.GetAll().ToList().Select(e=> new InventorylogViewModel(e)).ToList();
            return Ok(inv);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(InventorylogViewModel))]
        public IHttpActionResult GetInventory(Guid id)
        {
            InventorylogViewModel inventory = new InventorylogViewModel (_inventorylogRepository.GetSingle(e => e.Id == id));
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(Guid id, InventorylogViewModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.Id)
            {
                return BadRequest();
            }

            try
            {
                _inventorylogRepository.Edit(inventory.ToModel());

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExist(id))
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
        [ResponseType(typeof(InventorylogViewModel))]
        public IHttpActionResult PostInventory(InventorylogViewModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _inventorylogRepository.Add(inventory.ToModel());
            }
            catch (DbUpdateException)
            {
                if (InventoryExist(inventory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Inventorylog))]
        public async Task<IHttpActionResult> DeleteInventory(Guid id)
        {
            Inventorylog inventory = _inventorylogRepository.GetSingle(e => e.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            await _inventorylogRepository.DeleteAsync(inventory);


            return Ok(inventory);
        }



        private bool InventoryExist(Guid id)
        {
            IQueryable<Inventorylog> inv = _inventorylogRepository.GetAll();
            return inv.Count(e => e.Id == id) > 0;
        }
    }
}