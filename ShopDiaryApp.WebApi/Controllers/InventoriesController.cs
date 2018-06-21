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
using ShopDiaryProject.Repository.Purchase;

namespace ShopDiaryApp.API.Controllers
{
    public class InventoriesController : ApiController
    {
        private IInventoryRepository _inventoryRepository;

        public InventoriesController()
        {
            _inventoryRepository = new InventoryRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetInventories()
        {
            IQueryable<Inventory> inv = _inventoryRepository.GetAll();
            return Ok(inv);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult GetInventory(Guid id)
        {
            Inventory inventory = _inventoryRepository.GetSingle(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(Guid id, Inventory inventory)
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
                _inventoryRepository.Edit(inventory);

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
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult PostInventory(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _inventoryRepository.Add(inventory);
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
        public IHttpActionResult DeleteInventory(Guid id)
        {
            Inventory inventory = _inventoryRepository.GetSingle(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _inventoryRepository.Delete(inventory);


            return Ok(inventory);
        }



        private bool InventoryExist(Guid id)
        {
            IQueryable<Inventory> inv = _inventoryRepository.GetAll();
            return inv.Count(e => e.Id == id) > 0;
        }
    }
}