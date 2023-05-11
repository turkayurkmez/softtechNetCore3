using Courses.DataOperations.Data;
using Courses.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.DataOperations.Repositories
{
    public class EFCourseRepository : ICourseRepository
    {
        private readonly CoursesCatalogDbContext dbContext;

        public EFCourseRepository(CoursesCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(Course entity)
        {
            await dbContext.Courses.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var course = dbContext.Courses.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                dbContext.Remove(course);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetAsync(int id)
        {
            return await dbContext.Courses.FindAsync(id);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await dbContext.Courses.AsNoTracking().AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> SeachByCategory(int id)
        {
            return await dbContext.Courses.AsNoTracking().Where(c => c.CategoryId == id).ToListAsync();

        }

        public async Task<IEnumerable<Course>> SeachByName(string name)
        {
            return await dbContext.Courses.AsNoTracking().Where(c => c.Name.Contains(name)).ToListAsync();
        }

        public async Task Update(Course entity)
        {
            dbContext.Courses.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
