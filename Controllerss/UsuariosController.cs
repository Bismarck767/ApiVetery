using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models;
using WebApplication1.DTO;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] CrearUsuarioDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Username == dto.NombreUsuario))
            {
                return BadRequest("El nombre de usuario ya existe.");
            }

            var usuario = new Usuario
            {
                Username = dto.NombreUsuario,  // ← CAMBIO: era dto.Username
                PasswordHash = HashPassword(dto.Contraseña),  // ← CAMBIO: era dto.Password
                Role = "employee"  // ← CAMBIO: rol fijo porque el frontend no lo envía
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == dto.NombreUsuario);  // ← CAMBIO: era dto.NombreUsuario

            if (usuario == null || usuario.PasswordHash != HashPassword(dto.Contraseña))  // ← CAMBIO: era dto.Contraseña
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            return Ok(new
            {
                mensaje = "Login exitoso",
                UserId = usuario.Id,  // ← AGREGADO: para el frontend
                Username = usuario.Username,
                Role = usuario.Role,
                Token = $"token_{usuario.Id}_{DateTime.Now.Ticks}"  // ← AGREGADO: token simple
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Logout exitoso");  // ← SIMPLIFICADO
        }

        [HttpGet("ping")]  // ← AGREGADO: para despertar la API
        public IActionResult Ping()
        {
            return Ok("API funcionando");
        }

        private string HashPassword(string contraseña)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(contraseña);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
