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
using ShopDiaryApp.API.Models.ViewModels;
using System.Threading.Tasks;

namespace ShopDiaryApp.API.Controllers
{
    public class ConsumesController : ApiController
    {
        private ConsumeRepository _consumeRepository;

        public ConsumesController()
        {
            _consumeRepository = new ConsumeRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetConsume()
        {
            IEnumerable<ConsumeViewModel> con = _consumeRepository.GetAll().ToList().Select(e => new ConsumeViewModel(e)).ToList();
            return Ok(con);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(ConsumeViewModel))]
        public IHttpActionResult GetConsume(Guid id)
        {
            ConsumeViewModel consume = new ConsumeViewModel(_consumeRepository.GetSingle(e => e.Id == id));
            if (consume == null)
            {
                return NotFound();
            }

            return Ok(consume);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutConsume(Guid id, ConsumeViewModel consume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consume.Id)
            {
                return BadRequest();
            }
            consume.Id = id;
            try
            {
                await _consumeRepository.EditAsync(consume.ToModel());

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumeExist(id))
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
        [ResponseType(typeof(ConsumeViewModel))]
        public IHttpActionResult PostConsume(ConsumeViewModel consume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _consumeRepository.Add(consume.ToModel());
            }
            catch (DbUpdateException)
            {
                if (ConsumeExist(consume.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = consume.Id }, consume);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Consume))]
        public async Task<IHttpActionResult> DeleteConsume(Guid id)
        {
            Consume consume = _consumeRepository.GetSingle(e => e.Id == id);
            if (consume == null)
            {
                return NotFound();
            }

            await _consumeRepository.DeleteAsync(consume);


            return Ok(consume);
        }



        private bool ConsumeExist(Guid id)
        {
            IQueryable<Consume> con = _consumeRepository.GetAll();
            return con.Count(e => e.Id == id) > 0;
        }
    }
}