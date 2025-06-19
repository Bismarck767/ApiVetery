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
            if (await _context.Usuarios.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("El nombre de usuario ya existe.");
            }

            var usuario = new Usuario
            {
                Username = dto.Username,  // ← VOLVIÓ AL ORIGINAL
                PasswordHash = HashPassword(dto.Password),  // ← VOLVIÓ AL ORIGINAL
                Role = dto.Role ?? "employee"  // ← Usar el rol o "employee" por defecto
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == dto.NombreUsuario);

            if (usuario == null || usuario.PasswordHash != HashPassword(dto.Contraseña))
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            return Ok(new
            {
                mensaje = "Login exitoso",
                UserId = usuario.Id,
                Username = usuario.Username,
                Role = usuario.Role,
                Token = $"token_{usuario.Id}_{DateTime.Now.Ticks}"
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()  // ← QUITADO async
        {
            return Ok("Logout exitoso");
        }

        [HttpGet("ping")]
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
