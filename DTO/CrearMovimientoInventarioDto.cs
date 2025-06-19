namespace WebApplication1.DTO
{
    public class CrearMovimientoInventarioDto
    {
        public int MedicamentoId { get; set; }
        public int Cantidad { get; set; } // positiva o negativa
        public string TipoMovimiento { get; set; } = string.Empty; // "Entrada" o "Salida"
        public string Usuario { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }

}
