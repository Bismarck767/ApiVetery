namespace WebApplication1.DTO
{
    public class MedicamentoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string TipoAnimal { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }

        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; } = string.Empty;
    }

}
