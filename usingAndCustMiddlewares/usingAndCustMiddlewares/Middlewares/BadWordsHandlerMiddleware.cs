namespace usingAndCustMiddlewares.Middlewares
{
    /// <summary>
    /// Eğer json içinde kötü kelime varsa; response olarak 400 (BadRequest) döndürür ve hata bildirir
    /// </summary>
    public class BadWordsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public BadWordsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var jsonBody = (string?)context.Items["JsonBody"];

            var badWords = new List<string> { "pis", "kaka", "kötü", "salak" };
            bool isContainBadWords = badWords.Any(word => jsonBody.Contains(word));
            if (isContainBadWords)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Post'da kabul edilmeyen kelimeler var" });
                return;
            }
            await _next(context);
        }
    }
}
