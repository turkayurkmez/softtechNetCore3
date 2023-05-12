using Courses.DataOperations.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Tests
{
    public class InMemoryWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                   .ConfigureTestServices(services =>
                   {
                       //TODO 4: Uygulamayı test edebilmek için gereken (db dahil) tüm instance'ları burada konfigüre et.
                       var option = new DbContextOptionsBuilder<CoursesCatalogDbContext>()
                                        .UseInMemoryDatabase("TestDb").Options;
                       services.AddScoped<CoursesCatalogDbContext>(opt => new CourseTestContext(option));
                       using var scope = services.BuildServiceProvider().CreateScope();
                       var scopedService = scope.ServiceProvider;
                       var db = scopedService.GetRequiredService<CoursesCatalogDbContext>();
                       db.Database.EnsureCreated();


                   });
        }
    }
}
