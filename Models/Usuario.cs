namespace WebApplication1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Empleado"; // "Administrador" o "Empleado"

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }



}
