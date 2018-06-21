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
    public class ConsumesController : ApiController
    {
        private IConsumeRepository _consumeRepository;

        public ConsumesController()
        {
            _consumeRepository = new ConsumeRepository();
        }

        // GET: api/Categories
        public IHttpActionResult GetConsume()
        {
            IQueryable<Consume> con = _consumeRepository.GetAll();
            return Ok(con);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Consume))]
        public IHttpActionResult GetConsume(Guid id)
        {
            Consume consume = _consumeRepository.GetSingle(id);
            if (consume == null)
            {
                return NotFound();
            }

            return Ok(consume);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConsume(Guid id, Consume consume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != consume.Id)
            {
                return BadRequest();
            }

            try
            {
                _consumeRepository.Edit(consume);

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
        [ResponseType(typeof(Consume))]
        public IHttpActionResult PostConsume(Consume consume)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            try
            {
                _consumeRepository.Add(consume);
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
        public IHttpActionResult DeleteConsume(Guid id)
        {
            Consume consume = _consumeRepository.GetSingle(id);
            if (consume == null)
            {
                return NotFound();
            }

            _consumeRepository.Delete(consume);


            return Ok(consume);
        }



        private bool ConsumeExist(Guid id)
        {
            IQueryable<Consume> con = _consumeRepository.GetAll();
            return con.Count(e => e.Id == id) > 0;
        }
    }
}