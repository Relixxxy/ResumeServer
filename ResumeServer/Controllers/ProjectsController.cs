using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Models;
using ResumeServer.Repository;

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IRepository<UserProject> _projectRepository;

        public ProjectsController(IRepository<UserProject> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProject>>> GetProjects()
        {
            IEnumerable<UserProject> projects = await _projectRepository.GetAllAsync();

            if (projects == null)
                return NotFound();

            return projects.ToList();
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProject>> GetProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
                return NotFound();

            return project;
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ActionResult<UserProject>> Post(UserProject project)
        {
            await _projectRepository.AddAsync(project);

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserProject project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            try
            {
                await _projectRepository.EditAsync(id, project);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_projectRepository.EntityExists(id))
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

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
