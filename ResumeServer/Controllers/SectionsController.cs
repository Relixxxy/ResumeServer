using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;
using ResumeServer.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IRepository<Section> _sectionRepository;

        public SectionsController(IRepository<Section> sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }


        // GET: api/<SectionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            IEnumerable<Section> sections = await _sectionRepository.GetAllAsync();

            if (sections == null)
                return NotFound();

            return sections.ToList();
        }

        // GET api/<SectionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _sectionRepository.GetByIdAsync(id);

            if (section == null)
                return NotFound();

            return section;
        }

        // POST api/<SectionsController>
        [HttpPost]
        public async Task<ActionResult<Section>> Post(Section section)
        {
            await _sectionRepository.AddAsync(section);

            return CreatedAtAction("GetSection", new { id = section.Id }, section);
        }

        // PUT api/<SectionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Section section)
        {
            if (id != section.Id)
            {
                return BadRequest();
            }

            try
            {
                await _sectionRepository.EditAsync(id, section);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_sectionRepository.EntityExists(id))
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

        // DELETE api/<SectionsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sectionRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
