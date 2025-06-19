namespace WebApplication1.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string TipoAnimal { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }

        // FK Proveedor
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }


}
