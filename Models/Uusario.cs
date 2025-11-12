using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class Usuario
    {
        [Key]
        public string Dni { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Rol { get; set; } = "cliente";
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public ICollection<Reserva>? Reservas { get; set; }
    }
}
