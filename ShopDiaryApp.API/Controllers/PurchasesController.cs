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
using ShopDiaryProject.Repository;
using ShopDiaryProject.Repository.Inventory;
using ShopDiaryProject.Domain.ViewModels;
using System.Threading.Tasks;

namespace ShopDiaryApp.API.Controllers
{
    public class PurchasesController : ApiController
    {
        private PurchaseRepository _purchaseRepository;

        public PurchasesController()
        {
            _purchaseRepository = new PurchaseRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetPurchases()
        {
            IEnumerable < PurchaseViewModel> pur = _purchaseRepository.GetAll().ToList().Select(e => new PurchaseViewModel(e)).ToList();
            return Ok(pur);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(Guid id)
        {
            Purchase purchase = _purchaseRepository.GetSingle(e => e.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPurchase(Guid id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }
            purchase.Id = id;
            try
            {
                await _purchaseRepository.EditAsync(purchase);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExist(id))
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
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult PostPurchases(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _purchaseRepository.Add(purchase);
            }
            catch (DbUpdateException)
            {
                if (PurchaseExist(purchase.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = purchase.Id }, purchase);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Purchase))]
        public async Task<IHttpActionResult> DeletePurchase(Guid id)
        {
            Purchase purchase = _purchaseRepository.GetSingle(e => e.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            await _purchaseRepository.DeleteAsync(purchase);


            return Ok(purchase);
        }



        private bool PurchaseExist(Guid id)
        {
            IQueryable<Purchase> pur = _purchaseRepository.GetAll();
            return pur.Count(e => e.Id == id) > 0;
        }
    }
}