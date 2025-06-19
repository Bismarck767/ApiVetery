using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;

using WebApplication1.DTO;
using WebApplication1.Models;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientosInventarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventarioDto>>> GetAll()
        {
            var movimientos = await _context.MovimientosInventario
                .Include(m => m.Medicamento)
            .OrderByDescending(m => m.Fecha)
                .Select(m => new MovimientoInventarioDto
                {
                    Id = m.Id,
                    Fecha = m.Fecha,
                    Cantidad = m.Cantidad,
                    TipoMovimiento = m.TipoMovimiento,
                    Observaciones = m.Observaciones,
                    MedicamentoNombre = m.Medicamento.Nombre
                })
                .ToListAsync();

            return Ok(movimientos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] CrearMovimientoInventarioDto dto)
        {
            var medicamento = await _context.Medicamentos.FindAsync(dto.MedicamentoId);
            if (medicamento == null)
                return NotFound("El medicamento especificado no existe.");

            // Actualizar stock según el tipo de movimiento
            if (dto.TipoMovimiento.ToLower() == "entrada")
            {
                medicamento.Cantidad += dto.Cantidad;
            }
            else if (dto.TipoMovimiento.ToLower() == "salida")
            {
                if (medicamento.Cantidad < dto.Cantidad)
                    return BadRequest("No hay suficiente stock para realizar la salida.");

                medicamento.Cantidad -= dto.Cantidad;
            }
            else
            {
                return BadRequest("Tipo de movimiento inválido. Debe ser 'entrada' o 'salida'.");
            }

            var movimiento = new MovimientoInventario
            {
                MedicamentoId = dto.MedicamentoId,
                Fecha = DateTime.UtcNow,
                Cantidad = dto.Cantidad,
                TipoMovimiento = dto.TipoMovimiento,
                Observaciones = dto.Observaciones
            };

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Movimiento registrado exitosamente",
                nuevoStock = medicamento.Cantidad
            });

        }
    }
}
