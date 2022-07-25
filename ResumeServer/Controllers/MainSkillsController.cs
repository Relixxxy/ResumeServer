using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;
using ResumeServer.Repository;

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainSkillsController : ControllerBase
    {
        private readonly IRepository<MainSkill> _mainSkillRepository;

        public MainSkillsController(IRepository<MainSkill> mainSkillRepository)
        {
            _mainSkillRepository = mainSkillRepository;
        }

        // GET: api/<MainSkills>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MainSkill>>> GetMainSkills()
        {
            IEnumerable<MainSkill> mainSkills = await _mainSkillRepository.GetAllAsync();

            if (mainSkills == null)
                return NotFound();

            return mainSkills.ToList();
        }

        // GET api/<MainSkills>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MainSkill>> GetMainSkill(int id)
        {
            var mainSkill = await _mainSkillRepository.GetByIdAsync(id);

            if (mainSkill == null)
                return NotFound();

            return mainSkill;
        }

        // POST api/<MainSkills>
        [HttpPost]
        public async Task<ActionResult<MainSkill>> Post(MainSkill mainSkill)
        {
            mainSkill.Id = 0;
            await _mainSkillRepository.AddAsync(mainSkill);

            return CreatedAtAction("GetMainSkill", new { id = mainSkill.Id }, mainSkill);
        }

        // PUT api/<MainSkills>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MainSkill mainSkill)
        {
            if (id != mainSkill.Id)
            {
                return BadRequest();
            }

            try
            {
                await _mainSkillRepository.EditAsync(id, mainSkill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_mainSkillRepository.EntityExists(id))
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

        // DELETE api/<MainSkills>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mainSkillRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
