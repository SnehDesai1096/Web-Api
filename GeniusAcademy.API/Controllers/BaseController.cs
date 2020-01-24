using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeniusAcademy.EF.Respsitory;
using GeniusAcademy.Types;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeniusAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(long id)
        {
            var movie = await repository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TEntity movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            await repository.Update(movie);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity movie)
        {
            await repository.Add(movie);
            return CreatedAtAction("Get", new { id = movie.Id }, movie);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(long id)
        {
            var movie = await repository.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

    }
}