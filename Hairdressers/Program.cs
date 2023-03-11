using Hairdressers.Data;
using Hairdressers.Interfaces;
using Hairdressers.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
    });

string connectionstring = builder.Configuration.GetConnectionString("SqlHairdressersHome");

    builder.Services.AddTransient<IRepositoryHairdresser, RepositoryHairdresser>();
    builder.Services.AddDbContext<HairdressersContext> (
        options => options.UseSqlServer(connectionstring)
    );

builder.Services.AddAntiforgery();
builder.Services.AddControllersWithViews();
// Inyectamos el archivo appsettings.json como configuración del IConfiguration
builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
    app.UseStaticFiles();

    app.UseSession();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Landing}/{action=Index}"
    );

app.Run();
