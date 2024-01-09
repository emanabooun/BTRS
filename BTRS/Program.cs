using BTRS.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IServiceCollection serviceCollection = builder.Services.AddDbContext<SystemDbContext>(
    item =>
    item.UseSqlServer(builder.Configuration.GetConnectionString("conn"))
    );


builder.Services.AddSession(
    opt =>
    {
        opt.IdleTimeout = TimeSpan.FromMinutes(30);
    }
    );

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Passengers}/{action=login}/{id?}");

app.Run();
