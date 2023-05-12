using Courses.API.Extensions;
using Courses.API.Security;
using Courses.Application.Mapper;
using Courses.Application.Services;
using Courses.DataOperations.Data;
using Courses.DataOperations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));
var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<CoursesCatalogDbContext>(builder => builder.UseSqlServer(connectionString));
builder.Services.AddScoped<ICourseRepository, EFCourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching(option =>
{
    option.SizeLimit = 1000;
});
//TODO 1: Konfigure etmeyi unutma!
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddDistributedSqlServerCache(opt =>
{
    opt.ConnectionString = builder.Configuration.GetConnectionString("cacheDb");
    opt.SchemaName = "dbo";
    opt.TableName = "TestCache";

});

builder.Services.AddCors(option =>
{
    option.AddPolicy("allow", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication("Basic")
                .AddScheme<BasicAuthenticationOption, BasicAuthenticationHandler>("Basic", null);


var app = builder.Build();

//Uygulamanın lifeTime olaylarında caching ya da başka bir servis kullanmak isterseniz.
//app.Lifetime.ApplicationStarted.Register(() =>
//{
//    var currentTime = DateTime.Now.ToString();
//    var encodedTime = Encoding.UTF8.GetBytes(currentTime);
//    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(180));

//    app.Services.GetService<IDistributedCache>().Set("cachedOnApp_Start", encodedTime, option);
//    app.Logger.LogInformation("Uygulama başladı....");

//});
//Yukarıdaki kod yerine extension yazdık:
app.SetLifetimeProcess();

app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
    {
        MaxAge = TimeSpan.FromMinutes(1),
        Public = true

    };
    context.Response.Headers[HeaderNames.Vary] = new string[] { "Accept-Encoding" };
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseCors("allow");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program
{

}