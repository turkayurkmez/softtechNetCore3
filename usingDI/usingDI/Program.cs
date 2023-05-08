using usingDI.Models;
using usingDI.Services;
using usingDI.Tenants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IProductService, AProductService>();
//Singleton; app boyunca sadece 1 instance.... 
//Transient: her ihtiyaç duyulduğunda yeni instance
//Scoped: Aynı httpRequest'de fakat farklı objelerde aynı instance...

builder.Services.AddSingleton<ISingleton, Singleton>();
builder.Services.AddTransient<ITransient, Transient>();
builder.Services.AddScoped<IScoped, Scoped>();
builder.Services.AddTransient<GuidService>();


/*
 * Multi tenant bir sistemde; Müşterinin kullandığı altyapıya göre dependency injection yapılabilir.
 * 1. Tenant provider kullandık. Burada müşterinin tercih ettiği alt yapılar (SQLTenant, OracleTenant vs) var. 
 * 2. Seçilen tenant provider'a göre nesneye karar verdik: 
 */

builder.Services.AddScoped<ITenantService, SQLTenant>();

builder.Services.AddScoped<IDatabaseClient>(services =>
{
    var provider = services.GetRequiredService<ITenantService>();
    var tenant = provider.GetTenantId();
    if (tenant == "OracleConnection")
    {
        return new OracleClient();
    }
    else if (tenant == "SqlConnection")
    {
        return new SqlClient();
    }

    throw new ArgumentException($"Herhangi bir tenant servis eklenmemiş ya da tanınmıyor....");

});




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
