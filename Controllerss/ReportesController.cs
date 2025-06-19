using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using System.Globalization;
using System.Text;

namespace VeterinaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("consumo-mensual")]
        public async Task<IActionResult> GetConsumoMensual(int año, int mes)
        {
            var movimientos = await _context.MovimientosInventario
                .Include(m => m.Medicamento)
                .Where(m => m.Fecha.Year == año && m.Fecha.Month == mes && m.TipoMovimiento.ToLower() == "salida")
                .GroupBy(m => m.Medicamento.Nombre)
                .Select(g => new
                {
                    Medicamento = g.Key,
                    CantidadConsumida = g.Sum(x => x.Cantidad)
                })
                .ToListAsync();

            return Ok(movimientos);
        }

        [HttpGet("mas-utilizados")]
        public async Task<IActionResult> GetMasUtilizados()
        {
            var top = await _context.MovimientosInventario
                .Include(m => m.Medicamento)
                .Where(m => m.TipoMovimiento.ToLower() == "salida")
                .GroupBy(m => m.Medicamento.Nombre)
                .OrderByDescending(g => g.Sum(m => m.Cantidad))
                .Take(10)
                .Select(g => new
                {
                    Medicamento = g.Key,
                    TotalUsado = g.Sum(x => x.Cantidad)
                })
                .ToListAsync();

            return Ok(top);
        }

        [HttpGet("exportar-excel")]
        public async Task<IActionResult> ExportarCSV()
        {
            var datos = await _context.Medicamentos.ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Nombre,Código,Cantidad,StockMinimo,Dosis,TipoAnimal,FechaVencimiento,ProveedorId");

            foreach (var m in datos)
            {
                sb.AppendLine($"{m.Nombre},{m.Codigo},{m.Cantidad},{m.StockMinimo},{m.Dosis},{m.TipoAnimal},{m.FechaVencimiento.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)},{m.ProveedorId}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "medicamentos_export.csv");
        }
    }
}
