using Models.DataBaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get Connection string from Enviroment or Appsettings
var stringConnection = Environment.GetEnvironmentVariable("DefaultConnextion");
stringConnection = (string.IsNullOrEmpty( stringConnection)) ?  stringConnection = builder.Configuration.GetConnectionString("DefaultConnextion") : stringConnection ;

// Add services to the container.
builder.Services.AddControllersWithViews();
// SQL Server Dependency Injection
builder.Services.AddDbContext<WareHouseDataContext>(
    option => option.UseSqlServer(stringConnection)
);





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
