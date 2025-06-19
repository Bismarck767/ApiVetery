namespace WebApplication1.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty; // "Vencimiento", "StockBajo", etc.
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public bool Resuelta { get; set; } = false;

        public int? MedicamentoId { get; set; } // opcional
        public Medicamento? Medicamento { get; set; }
    }

}
