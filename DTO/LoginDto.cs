namespace WebApplication1.DTO
{
    public class LoginDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
    }

    public class CrearEditarUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = "Empleado"; // Empleado por defecto
    }
}
