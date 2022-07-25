using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;
using ResumeServer.Repository;

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionSkillsController : ControllerBase
    {
        private readonly IRepository<AdditionSkill> _additionSkillRepository;

        public AdditionSkillsController(IRepository<AdditionSkill> additionSkillRepository)
        {
            _additionSkillRepository = additionSkillRepository;
        }

        // GET: api/<AdditionSkills>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdditionSkill>>> GetAdditionSkills()
        {
            IEnumerable<AdditionSkill> additionSkills = await _additionSkillRepository.GetAllAsync();

            if (additionSkills == null)
                return NotFound();

            return additionSkills.ToList();
        }

        // GET api/<AdditionSkills>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionSkill>> GetAdditionSkill(int id)
        {
            var additionSkill = await _additionSkillRepository.GetByIdAsync(id);

            if (additionSkill == null)
                return NotFound();

            return additionSkill;
        }

        // POST api/<AdditionSkills>
        [HttpPost]
        public async Task<ActionResult<AdditionSkill>> Post(AdditionSkill additionSkill)
        {
            additionSkill.Id = 0;
            await _additionSkillRepository.AddAsync(additionSkill);

            return CreatedAtAction("GetAdditionSkill", new { id = additionSkill.Id }, additionSkill);
        }

        // PUT api/<AdditionSkills>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AdditionSkill additionSkill)
        {
            if (id != additionSkill.Id)
            {
                return BadRequest();
            }

            try
            {
                await _additionSkillRepository.EditAsync(id, additionSkill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_additionSkillRepository.EntityExists(id))
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

        // DELETE api/<AdditionSkills>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _additionSkillRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
