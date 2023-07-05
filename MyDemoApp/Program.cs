using MyDemoApp.Models;
using Microsoft.EntityFrameworkCore;
using MyDemoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connstr = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(connstr), ServiceLifetime.Singleton
);

builder.Services.AddSingleton<IEmployeeRepo, EmployeeRepo>();
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
