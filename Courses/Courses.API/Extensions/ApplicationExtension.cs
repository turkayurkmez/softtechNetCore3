using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Courses.API.Extensions
{
    public static class ApplicationExtension
    {
        public static IApplicationBuilder SetLifetimeProcess(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                var currentTime = DateTime.Now.ToString();
                var encodedTime = Encoding.UTF8.GetBytes(currentTime);
                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(180));

                app.Services.GetService<IDistributedCache>().Set("cachedOnApp_Start", encodedTime, option);
                app.Logger.LogInformation("Uygulama başladı....");

            });

            return app;
        }
    }
}
