using OptionPattern.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.AddJsonFile($"appsettings.Development.json", true, true);

//Eğer, app.settings içerisinden okuyacağınız değerler; IServiceCollection içine gönderilecekse; okuma işlemini burada yapmalısınız.
//Eğer sadece bir middleware içinde kullanacaksanız; //var app = builder.Build(); satırından sonra da kullanabilirsiniz.

var setting = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
//ihtiyaç varsa; nesne ekleniyor:
builder.Services.AddSingleton<SmtpSettings>(setting);


var app = builder.Build();

var appSetting = app.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();


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
