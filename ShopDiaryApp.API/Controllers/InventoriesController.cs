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

namespace ShopDiaryApp.API.Controllers
{
    public class InventoriesController : ApiController
    {
        private InventoryRepository _inventoryRepository;

        public InventoriesController()
        {
            _inventoryRepository = new InventoryRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetInventories()
        {
            IEnumerable<InventoryViewModel> inv = _inventoryRepository.GetAll().ToList().Select(e=> new InventoryViewModel(e)).ToList();
            return Ok(inv);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(InventoryViewModel))]
        public IHttpActionResult GetInventory(Guid id)
        {
            InventoryViewModel inventory = new InventoryViewModel (_inventoryRepository.GetSingle(e => e.Id == id));
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(Guid id, InventoryViewModel inventory)
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
                _inventoryRepository.Edit(inventory.ToModel());

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
        [ResponseType(typeof(InventoryViewModel))]
        public IHttpActionResult PostInventory(InventoryViewModel inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _inventoryRepository.Add(inventory.ToModel());
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
        [ResponseType(typeof(Inventory))]
        public async Task<IHttpActionResult> DeleteInventory(Guid id)
        {
            Inventory inventory = _inventoryRepository.GetSingle(e => e.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            await _inventoryRepository.DeleteAsync(inventory);


            return Ok(inventory);
        }



        private bool InventoryExist(Guid id)
        {
            IQueryable<Inventory> inv = _inventoryRepository.GetAll();
            return inv.Count(e => e.Id == id) > 0;
        }
    }
}