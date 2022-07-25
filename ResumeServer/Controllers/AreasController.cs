using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;
using ResumeServer.Repository;

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IRepository<Area> _areaRepository;

        public AreasController(IRepository<Area> areaRepository)
        {
            _areaRepository = areaRepository;
        }

        // GET: api/<Areas>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> Getareas()
        {
            IEnumerable<Area> areas = await _areaRepository.GetAllAsync();

            if (areas == null)
                return NotFound();

            return areas.ToList();
        }

        // GET api/<Areas>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> Getarea(int id)
        {
            var area = await _areaRepository.GetByIdAsync(id);

            if (area == null)
                return NotFound();

            return area;
        }

        // POST api/<Areas>
        [HttpPost]
        public async Task<ActionResult<Area>> Post(Area area)
        {
            await _areaRepository.AddAsync(area);

            return CreatedAtAction("Getarea", new { id = area.Id }, area);
        }

        // PUT api/<Areas>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            try
            {
                await _areaRepository.EditAsync(id, area);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_areaRepository.EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<Areas>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _areaRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
