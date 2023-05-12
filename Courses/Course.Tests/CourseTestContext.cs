using Courses.DataOperations.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Course.Tests
{
    public class CourseTestContext : CoursesCatalogDbContext
    {
        public CourseTestContext(DbContextOptions<CoursesCatalogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            seedData<Courses.Entities.Course>(modelBuilder, "../../../data/courses.json");
            //base.OnModelCreating(modelBuilder);
        }

        private void seedData<T>(ModelBuilder modelBuilder, string file) where T : class
        {
            using StreamReader reader = new StreamReader(file);
            string json = reader.ReadToEnd();
            var data = JsonConvert.DeserializeObject<List<T>>(json);
            modelBuilder.Entity<T>().HasData(data);
        }


    }
}
