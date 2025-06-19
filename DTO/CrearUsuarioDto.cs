namespace WebApplication1.DTO
{
    public class CrearUsuarioDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Empleado"; // default
    }

}
