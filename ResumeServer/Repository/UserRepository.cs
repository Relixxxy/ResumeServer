using Microsoft.EntityFrameworkCore;
using ResumeServer.Data;
using ResumeServer.Models;

namespace ResumeServer.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ResumeContext _context;

        public UserRepository(ResumeContext context)
        {
            _context = context;
        }  
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                await SetUserDependenciesAsync(user);
            }

            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            await SetUserDependenciesAsync(user);

            return user;
        }

        private async Task SetUserDependenciesAsync(User user)
        {
            if (user == null)
                return;

            user.Projects = await _context.UserProjects.Where(p => p.UserId == user.Id).ToListAsync();
            user.Areas = await _context.Areas.Where(a => a.UserId == user.Id).ToListAsync();
            user.MainSkills = await _context.MainSkills.Where(s => s.UserId == user.Id).ToListAsync();
            user.AdditionSkills = await _context.AdditionSkills.Where(s => s.UserId == user.Id).ToListAsync();
            user.Sections = await _context.Sections.Where(s => s.UserId == user.Id).ToListAsync();
        }


        public async Task AddAsync(User entity)
        {
            if (entity == null)
                return;

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, User entity)
        {
            //User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            //user = entity;
            //_context.Entry(user).State = EntityState.Modified;
            _context.Entry(entity).State = EntityState.Modified;// cause i transfer full object fron db
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _context.Users.FirstOrDefault(e => e.Id == id) != null;
        }
    }
}
