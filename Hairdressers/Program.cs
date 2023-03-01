var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();

    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
    });

// Inyectamos el archivo appsettings.json como configuración del IConfiguration
builder.Configuration.AddJsonFile("appsettings.json");

var app = builder.Build();
    app.UseStaticFiles();

    app.UseSession();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Landing}/{action=Index}"
        //pattern: "{controller=Email}/{action=SendConfirmationEmail}"
    );

app.Run();
