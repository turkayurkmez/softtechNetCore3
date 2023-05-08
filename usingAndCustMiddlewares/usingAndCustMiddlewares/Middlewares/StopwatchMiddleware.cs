using System.Diagnostics;

namespace usingAndCustMiddlewares.Middlewares
{
    public class StopwatchMiddleware
    {
        private readonly RequestDelegate _next;

        public StopwatchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await _next(context);
            stopWatch.Stop();
            var miliseconds = stopWatch.ElapsedMilliseconds;
            await context.Response.WriteAsync($"Harcanan süre {miliseconds} milisaniye");

        }
    }
}
