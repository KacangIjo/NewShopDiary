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
    public class ProductsController : ApiController
    {
        private ProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository();
        }

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            IEnumerable<ProductViewModel> pro = _productRepository.GetAll().ToList().Select(e=> new ProductViewModel(e)).ToList();
            return Ok(pro);
        }

        // GET: api/product/5
        [ResponseType(typeof(ProductViewModel))]
        public IHttpActionResult GetProduct(Guid id)
        {
            ProductViewModel product =new ProductViewModel( _productRepository.GetSingle(e => e.Id == id));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            product.Id = id;
            try
            {
               await _productRepository.EditAsync(product);

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
        [ResponseType(typeof(ProductViewModel))]
        public IHttpActionResult PostProduct(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _productRepository.Add(product.ToModel());
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
        public async Task<IHttpActionResult> DeleteProduct(Guid id)
        {
            Product product = _productRepository.GetSingle(e => e.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product);


            return Ok(product);
        }



        private bool ProductExist(Guid id)
        {
            IQueryable<Product> pro = _productRepository.GetAll();
            return pro.Count(e => e.Id == id) > 0;
        }
    }
}