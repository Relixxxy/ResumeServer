using Microsoft.EntityFrameworkCore;
using ResumeServer.Data;
using ResumeServer.Models;

namespace ResumeServer.Repository
{
    public class CredentialsRepository : IRepository<Credentials>
    {
        private readonly ResumeContext _context;

        public CredentialsRepository(ResumeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Credentials>> GetAllAsync()
        {
            var creds = await _context.Credentials.ToListAsync();

            foreach (var cred in creds)
            {
                await SetCredentialsDependenciesAsync(cred);
            }

            return creds;
        }

        public async Task<Credentials> GetByIdAsync(int id)
        {
            var cred = await _context.Credentials.FirstOrDefaultAsync(c => c.Id == id);

            await SetCredentialsDependenciesAsync(cred);

            return cred;
        }

        public async Task AddAsync(Credentials entity)
        {
            if (entity == null)
                return;

            await _context.Credentials.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(await _context.Credentials.FirstOrDefaultAsync(c => c.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, Credentials entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _context.Users.FirstOrDefault(e => e.Id == id) != null;
        }

        public async Task<Credentials> LoginAsync(string login, string password)
        {
            var cred = await _context.Credentials.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);

            await SetCredentialsDependenciesAsync(cred);

            return cred;
        }

        private async Task SetCredentialsDependenciesAsync(Credentials cred)
        {
            if (cred == null)
                return;

            cred.User = await _context.Users.FirstOrDefaultAsync(u => u.CredentialsId == cred.Id);

            cred.User.Projects = await _context.UserProjects.Where(p => p.UserId == cred.User.Id).ToListAsync();
            cred.User.Areas = await _context.Areas.Where(a => a.UserId == cred.User.Id).ToListAsync();
            cred.User.MainSkills = await _context.MainSkills.Where(s => s.UserId == cred.User.Id).ToListAsync();
            cred.User.AdditionSkills = await _context.AdditionSkills.Where(s => s.UserId == cred.User.Id).ToListAsync();
            cred.User.Sections = await _context.Sections.Where(s => s.UserId == cred.User.Id).ToListAsync();
        }
        

        public async Task<Credentials> RegCheckAsync(string login)
        {
            var cred = await _context.Credentials.FirstOrDefaultAsync(c => c.Login == login);

            return cred;
        }
    }
}
