using Courses.Entities;

namespace Courses.DataOperations.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> SeachByName(string name);
        Task<IEnumerable<Course>> SeachByCategory(int id);

    }
}
