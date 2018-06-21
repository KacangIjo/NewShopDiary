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
using ShopDiaryProject.Repository.Consume;
using System.Threading.Tasks;
using ShopDiaryApp.API.Models.ViewModels;

namespace ShopDiaryApp.API.Controllers
{
    public class CategoriesController : ApiController
    {
        private CategoryRepository _categoryRepository;

        public CategoriesController()
        {
            _categoryRepository = new CategoryRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetCategories()
        {
            IEnumerable<CategoryViewModel> cat = _categoryRepository.GetAll().ToList().Select(e => new CategoryViewModel(e)).ToList();
            return Ok(cat);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoryViewModel))]
        public IHttpActionResult GetCategory(Guid id)
        {
            CategoryViewModel category = new CategoryViewModel( _categoryRepository.GetSingle(e => e.Id == id));
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(Guid id, CategoryViewModel category)
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
                _categoryRepository.Edit(category.ToModel());
               
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
        [ResponseType(typeof(CategoryViewModel))]
        public IHttpActionResult PostCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            try
            {
                _categoryRepository.Add(category.ToModel());
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
        public async Task<IHttpActionResult> DeleteCategory(Guid id)
        {
            Category location = _categoryRepository.GetSingle(e => e.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(location);

            return Ok(location);
        }

     

        private bool CategoryExists(Guid id)
        {
            IQueryable<Category> cat = _categoryRepository.GetAll();
            return cat.Count(e => e.Id == id) > 0;
        }
    }
}