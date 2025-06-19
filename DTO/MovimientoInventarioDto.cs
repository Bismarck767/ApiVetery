namespace WebApplication1.DTO
{
    public class MovimientoInventarioDto
    {
        public int Id { get; set; }
        public int MedicamentoId { get; set; }
        public string MedicamentoNombre { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }

}
