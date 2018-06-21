using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.Repository;

namespace ShopDiaryApp.API.Controllers
{
    public class PurchasesController : ApiController
    {
        private IPurchaseRepository _purchaseRepository;

        public PurchasesController()
        {
            _purchaseRepository = new PurchaseRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetPurchases()
        {
            IQueryable<Purchase> pur = _purchaseRepository.GetAll();
            return Ok(pur);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Purchase))]
        public IHttpActionResult GetPurchase(Guid id)
        {
            Purchase purchase = _purchaseRepository.GetSingle(id);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(purchase);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase(Guid id, Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase.Id)
            {
                return BadRequest();
            }

            try
            {
                _purchaseRepository.Edit(purchase);

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
        public IHttpActionResult DeletePurchase(Guid id)
        {
            Purchase purchase = _purchaseRepository.GetSingle(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _purchaseRepository.Delete(purchase);


            return Ok(purchase);
        }



        private bool PurchaseExist(Guid id)
        {
            IQueryable<Purchase> pur = _purchaseRepository.GetAll();
            return pur.Count(e => e.Id == id) > 0;
        }
    }
}