namespace Clase1.DTOs
{
    public class EmpleadoDTO
    {
        public string Nombre { get; set; } = string.Empty; // valor por defecto

        public string Apellido { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Dui { get; set; } = string.Empty;

        public string NumeroTelefonico { get; set; } = string.Empty;

        public int TipoEmpleadoId { get; set; }
    }
}
