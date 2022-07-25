using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeServer.Data;
using ResumeServer.Models;
using ResumeServer.Repository;

namespace ResumeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly CredentialsRepository _credentialsRepository;

        public CredentialsController(CredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credentials>>> GetUsers()
        {
            IEnumerable<Credentials> creds = await _credentialsRepository.GetAllAsync();

            return creds == null ? NotFound() : creds.ToList();
        }

        // GET: api/Credentials/5
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Credentials>> GetUser(string login, string password)
        {
            Credentials cred = await _credentialsRepository.LoginAsync(login, password);

            if (cred == null)
                return NotFound();

            return cred;
        }
        [HttpGet("{login}")]
        public async Task<ActionResult<Credentials>> GetUser(string login)
        {
            Credentials cred = await _credentialsRepository.RegCheckAsync(login);
         
            if (cred == null)
                return new Credentials();

            return cred;
        }

        // PUT: api/Credentials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Credentials cred)
        {
            if (id != cred.Id)
            {
                return BadRequest();
            }

            try
            {
                await _credentialsRepository.EditAsync(id, cred);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_credentialsRepository.EntityExists(id))
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

        // POST: api/Credentials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credentials>> PostUser(Credentials cred)
        {
            await _credentialsRepository.AddAsync(cred);

            return CreatedAtAction("GetCredentials", new { id = cred.Id }, cred);
        }

        // DELETE: api/Credentials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _credentialsRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}