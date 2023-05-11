using Courses.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.DataOperations.Data
{
    public class CoursesCatalogDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }


        public CoursesCatalogDbContext(DbContextOptions<CoursesCatalogDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                 new Category { Id = 1, Name = "Development" },
                 new Category { Id = 2, Name = "Art" },
                 new Category { Id = 3, Name = "Language" }
                 );



            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "C# Basics",
                    CategoryId = 1,
                    Description = "Learn with fun",
                    TotalHours = 50,
                    CourseImage = "https://loremflickr.com/320/240"
                },
                 new Course
                 {
                     Id = 2,
                     Name = "C# Advanced",
                     CategoryId = 1,
                     Description = "Learn Advanced C#",
                     TotalHours = 50,
                     CourseImage = "https://loremflickr.com/320/240"
                 },
                  new Course
                  {
                      Id = 3,
                      Name = "Painting Course",
                      CategoryId = 2,
                      Description = "Like Bob Ross :)",
                      TotalHours = 50,
                      CourseImage = "https://loremflickr.com/320/240"
                  },
                   new Course
                   {
                       Id = 4,
                       Name = "Spanish",
                       CategoryId = 3,
                       Description = "Learn Spanish",
                       TotalHours = 50,
                       CourseImage = "https://loremflickr.com/320/240"
                   }

            );


        }
    }
}
