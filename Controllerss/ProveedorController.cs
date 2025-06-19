using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;

using WebApplication1.DTO;
using WebApplication1.Models;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProveedoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetAll()
        {
            var proveedores = await _context.Proveedores
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Direccion = p.Direccion
                })
                .ToListAsync();

            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetById(int id)
        {
            var proveedor = await _context.Proveedores
                .Where(p => p.Id == id)
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Direccion = p.Direccion
                })
                .FirstOrDefaultAsync();

            if (proveedor == null)
                return NotFound();

            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearEditarProveedorDto dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Direccion = dto.Direccion
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proveedor creado exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CrearEditarProveedorDto dto)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound();

            proveedor.Nombre = dto.Nombre;
            proveedor.Telefono = dto.Telefono;
            proveedor.Email = dto.Email;
            proveedor.Direccion = dto.Direccion;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proveedor actualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound();

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proveedor eliminado" });
        }
    }
}
