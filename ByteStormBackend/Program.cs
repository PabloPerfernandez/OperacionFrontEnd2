using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ByteStormBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

// Configuración del contexto de la base de datos para SQLite
builder.Services.AddDbContext<ByteStormContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ByteStorm API", Version = "v1" });
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar CORS
app.UseCors("AllowFrontend");

// Habilitar Swagger y la UI de Swagger
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ByteStorm API v1");
    // c.RoutePrefix = string.Empty; // Descomenta esta línea si quieres que Swagger esté en la raíz
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();