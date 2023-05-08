using usingAndCustMiddlewares.Middlewares;

namespace usingAndCustMiddlewares.Extensions
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UseRejectBadWords(this IApplicationBuilder app)
        {
            app.UseMiddleware<JsonBodyMiddleware>();
            app.UseMiddleware<BadWordsHandlerMiddleware>();
            return app;
        }
    }
}
