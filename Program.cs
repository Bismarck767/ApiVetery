using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
// 🚀 Agregar controladores
builder.Services.AddControllers();

// 📦 Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "https://tu-frontend.netlify.app")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();
// Usar la policy
app.UseCors("AllowFrontend");
// Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔐 HTTPS
app.UseHttpsRedirection();

// 🌐 Usar CORS
app.UseCors("AllowAll");

// 🔑 Autenticación y autorización (cuando la agregues)
app.UseAuthorization();

// 🚀 Rutas de controladores
app.MapControllers();

app.Run();
