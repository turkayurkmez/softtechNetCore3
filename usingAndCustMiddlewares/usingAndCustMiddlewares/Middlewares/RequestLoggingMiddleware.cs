namespace usingAndCustMiddlewares.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            _logger.LogInformation($"Gelen request metodu:{httpContext.Request.Method}, adresi: {httpContext.Request.Path}");
            //oluşacak Response'u bellekte kopyasını aldık:
            var responseBodyStream = httpContext.Response.Body;
            //a001
            var responseStream = new MemoryStream();
            httpContext.Response.Body = responseStream;

            await _next(httpContext);
            responseStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseStream).ReadToEnd();
            _logger.LogInformation($"Response oluşturuldu...\n Yanıt olustugu an: {DateTime.Now.ToString()}");
            _logger.LogInformation($"Response:{httpContext.Response.StatusCode}\n{responseBody}");

            responseStream.Seek(0, SeekOrigin.Begin);
            await responseStream.CopyToAsync(responseBodyStream);

        }

    }
}
