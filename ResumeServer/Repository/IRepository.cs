using ResumeServer.Models;

namespace ResumeServer.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task EditAsync(int id, T entity);
        Task DeleteAsync(int id);
        bool EntityExists(int id);
    }
}
