using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
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

// Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ORDEN CORRECTO:
app.UseHttpsRedirection();          // 1. HTTPS
app.UseCors("AllowFrontend");       // 2. CORS (UNA SOLA VEZ)
app.UseAuthorization();             // 3. Autorización
app.MapControllers();               // 4. Controladores

app.Run();
