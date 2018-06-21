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
    public class ProductsController : ApiController
    {
        private IProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository();
        }

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            IQueryable<Product> pro = _productRepository.GetAll();
            return Ok(pro);
        }

        // GET: api/product/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(Guid id)
        {
            Product product = _productRepository.GetSingle(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                _productRepository.Edit(product);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExist(id))
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

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _productRepository.Add(product);
            }
            catch (DbUpdateException)
            {
                if (ProductExist(product.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(Guid id)
        {
            Product product = _productRepository.GetSingle(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(product);


            return Ok(product);
        }



        private bool ProductExist(Guid id)
        {
            IQueryable<Product> pro = _productRepository.GetAll();
            return pro.Count(e => e.Id == id) > 0;
        }
    }
}