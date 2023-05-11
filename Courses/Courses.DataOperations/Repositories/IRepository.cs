using Courses.Entities;

namespace Courses.DataOperations.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        //IEnumerable<T> GetAll();
        Task<T> GetAsync(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<bool> IsExistsAsync(int id);

    }
}
