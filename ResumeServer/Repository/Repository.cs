using Microsoft.EntityFrameworkCore;
using ResumeServer.Data;
using ResumeServer.Models;

namespace ResumeServer.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ResumeContext _context;
        DbSet<T> _entities;

        public Repository(ResumeContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity) 
        {
            if (entity == null)
                return;

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _entities.Remove(await _entities.FirstOrDefaultAsync(e => e.Id == id));
            await _context.SaveChangesAsync(); // mb add CancellationToken in async meth
        }

        public async Task EditAsync(int id, T entity)
        {
            T item = await _entities.FirstOrDefaultAsync(e => e.Id == id);
            item = entity;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool EntityExists(int id)
        {
            return _entities.FirstOrDefault(e => e.Id == id) != null;
        }
    }
}
