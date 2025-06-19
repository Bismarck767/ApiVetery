using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using WebApplication1.DTO;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("vencimientos-proximos")]
        public async Task<ActionResult<IEnumerable<AlertaDto>>> GetVencimientosProximos()
        {
            DateTime hoy = DateTime.Today;
            DateTime fechaLimite = hoy.AddDays(15);

            var alertas = await _context.Medicamentos
                .Where(m => m.FechaVencimiento <= fechaLimite && m.FechaVencimiento >= hoy)
                .Select(m => new AlertaDto
                {
                    MedicamentoId = m.Id,
                    MedicamentoNombre = m.Nombre,
                    Tipo = "Vencimiento próximo",
                    Mensaje = $"El medicamento '{m.Nombre}' vence pronto ({m.FechaVencimiento:dd/MM/yyyy})",
                    FechaGeneracion = DateTime.Now,
                    Resuelta = false
                })
                .ToListAsync();

            return Ok(alertas);
        }

        [HttpGet("vencidos")]
        public async Task<ActionResult<IEnumerable<AlertaDto>>> GetVencidos()
        {
            var vencidos = await _context.Medicamentos
                .Where(m => m.FechaVencimiento < DateTime.Today)
                .Select(m => new AlertaDto
                {
                    MedicamentoId = m.Id,
                    MedicamentoNombre = m.Nombre,
                    Tipo = "Vencido",
                    Mensaje = $"El medicamento '{m.Nombre}' ya está vencido ({m.FechaVencimiento:dd/MM/yyyy})",
                    FechaGeneracion = DateTime.Now,
                    Resuelta = false
                })
                .ToListAsync();

            return Ok(vencidos);
        }

        [HttpGet("stock-minimo")]
        public async Task<ActionResult<IEnumerable<AlertaDto>>> GetStockMinimo()
        {
            var alertas = await _context.Medicamentos
                .Where(m => m.Cantidad <= m.StockMinimo)
                .Select(m => new AlertaDto
                {
                    MedicamentoId = m.Id,
                    MedicamentoNombre = m.Nombre,
                    Tipo = "Stock mínimo",
                    Mensaje = $"El medicamento '{m.Nombre}' alcanzó su stock mínimo (Cantidad: {m.Cantidad})",
                    FechaGeneracion = DateTime.Now,
                    Resuelta = false
                })
                .ToListAsync();

            return Ok(alertas);
        }
    }
}
