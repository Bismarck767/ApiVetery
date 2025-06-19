using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllerss
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetAll()
        {
            var medicamentos = await _context.Medicamentos
                .Include(m => m.Proveedor)
                .Select(m => new MedicamentoDto
                {
                    Id = m.Id,
                    Codigo = m.Codigo,
                    Nombre = m.Nombre,
                    Cantidad = m.Cantidad,
                    StockMinimo = m.StockMinimo,
                    Dosis = m.Dosis,
                    FechaVencimiento = m.FechaVencimiento,
                    TipoAnimal = m.TipoAnimal,
                    ProveedorNombre = m.Proveedor.Nombre
                })
                .ToListAsync();

            return Ok(medicamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentoDto>> GetById(int id)
        {
            var medicamento = await _context.Medicamentos
                .Include(m => m.Proveedor)
                .Where(m => m.Id == id)
                .Select(m => new MedicamentoDto
                {
                    Id = m.Id,
                    Codigo = m.Codigo,
                    Nombre = m.Nombre,
                    Cantidad = m.Cantidad,
                    StockMinimo = m.StockMinimo,
                    Dosis = m.Dosis,
                    FechaVencimiento = m.FechaVencimiento,
                    TipoAnimal = m.TipoAnimal,
                    ProveedorNombre = m.Proveedor.Nombre
                })
                .FirstOrDefaultAsync();

            if (medicamento == null)
                return NotFound();

            return Ok(medicamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearEditarMedicamentoDto dto)
        {
            var medicamento = new Medicamento
            {
                Codigo = dto.Codigo,
                Nombre = dto.Nombre,
                Cantidad = dto.Cantidad,
                StockMinimo = dto.StockMinimo,
                Dosis = dto.Dosis,
                FechaVencimiento = dto.FechaVencimiento,
                TipoAnimal = dto.TipoAnimal,
                ProveedorId = dto.ProveedorId
            };

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Medicamento creado exitosamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CrearEditarMedicamentoDto dto)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
                return NotFound();

            medicamento.Codigo = dto.Codigo;
            medicamento.Nombre = dto.Nombre;
            medicamento.Cantidad = dto.Cantidad;
            medicamento.StockMinimo = dto.StockMinimo;
            medicamento.Dosis = dto.Dosis;
            medicamento.FechaVencimiento = dto.FechaVencimiento;
            medicamento.TipoAnimal = dto.TipoAnimal;
            medicamento.ProveedorId = dto.ProveedorId;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Medicamento actualizado" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
                return NotFound();

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Medicamento eliminado" });
        }
    }
}
