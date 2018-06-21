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
using ShopDiaryProject.Repository.Consume;

namespace ShopDiaryApp.API.Controllers
{
    public class CategoriesController : ApiController
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesController()
        {
            _categoryRepository = new CategoryRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetCategories()
        {
            IQueryable<Category> cat = _categoryRepository.GetAll();
            return Ok(cat);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(Guid id)
        {
            Category category = _categoryRepository.GetSingle(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(Guid id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                _categoryRepository.Edit(category);
               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            try
            {
                _categoryRepository.Add(category);
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(Guid id)
        {
            Category category = _categoryRepository.GetSingle(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(category);


            return Ok(category);
        }

     

        private bool CategoryExists(Guid id)
        {
            IQueryable<Category> cat = _categoryRepository.GetAll();
            return cat.Count(e => e.Id == id) > 0;
        }
    }
}