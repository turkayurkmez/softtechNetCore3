using customLifeTimeDI.LifeTimes;
using customLifeTimeDI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//1. Uygulama boyunca çalışacak lifecycle:
builder.Services.AddSingleton<IServiceScopeFactory, MyCustomLifeTime>();

//2.Belirtilen özel life cycle'da var olacak nesne tanımı:
builder.Services.Add(ServiceDescriptor.Singleton<ProductService>(services =>
{
    var customLifeTime = services.GetRequiredService<MyCustomLifeTime>();

    return new ProductService();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
