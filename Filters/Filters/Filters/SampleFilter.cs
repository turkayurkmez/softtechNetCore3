using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    /*
     * SampleFilter constructor'u dependency injection pattern'ini uyguluyor. Bunu herhangi bir attribute YAPAMAYACAĞI için
     * bu sınıfı yazıyoruz.
     */
    public class SampleFilter : IActionFilter
    {
        private readonly IConfiguration configuration;

        public SampleFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var connectionString = configuration.GetConnectionString("db");
        }
    }
}
