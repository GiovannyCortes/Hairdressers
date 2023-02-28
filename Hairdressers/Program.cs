var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();

    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options => {
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        //options.Cookie.IsEssential = true;
        //options.IdleTimeout = TimeSpan.FromSeconds(10);
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
