namespace WebApplication1.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; } = null!;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public int Cantidad { get; set; } // positiva (entrada) o negativa (salida)
        public string TipoMovimiento { get; set; } = string.Empty; // "Entrada", "Salida"
        public string Usuario { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }

}
