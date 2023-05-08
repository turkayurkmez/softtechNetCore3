using usingAndCustMiddlewares.Extensions;
using usingAndCustMiddlewares.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using var loggerFactory = LoggerFactory.Create(builder => { builder.AddSimpleConsole(); });
var logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();



var app = builder.Build();


//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("====");
//    await next();
//    await context.Response.WriteAsync("====");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync(">>>>");
//    await next();
//    await context.Response.WriteAsync("<<<<");
//});



//app.UseMiddleware<StopwatchMiddleware>();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseWelcomePage();
app.UseMiddleware<RequestLoggingMiddleware>(logger);
//app.UseMiddleware<JsonBodyMiddleware>();
//app.UseMiddleware<BadWordsHandlerMiddleware>();
//extension metot ile middleware'î daha hoş hale getirebilirsiniz.
app.UseRejectBadWords();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Merhaba Dünya");
//});

// ====>>>> Merhaba Dünya <<<<====

app.Run();
