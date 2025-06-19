namespace WebApplication1.DTO
{
    public class AlertaDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
        public bool Resuelta { get; set; }

        public int? MedicamentoId { get; set; }
        public string? MedicamentoNombre { get; set; }
    }

}
