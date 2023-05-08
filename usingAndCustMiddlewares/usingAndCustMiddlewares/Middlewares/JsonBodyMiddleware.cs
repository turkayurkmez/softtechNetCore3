namespace usingAndCustMiddlewares.Middlewares
{
    /// <summary>
    /// Bu sınıftan türeyen instance, gelen http request metodu post ise ve json içeriyorsa, bu datayı context içinde tutacak.
    /// </summary>
    public class JsonBodyMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonBodyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "POST" && context.Request.ContentType.StartsWith("application/json"))
            {
                using var streamReader = new StreamReader(context.Request.Body);
                var jsonBody = await streamReader.ReadToEndAsync();
                context.Items["JsonBody"] = jsonBody;
            }
            await _next(context);
        }
    }
}
