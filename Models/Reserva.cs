using System.ComponentModel.DataAnnotations;

namespace Reservas.Api.Models
{
    public class Reserva
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public string Estado { get; set; } = "pendiente";
        public string? Observaciones { get; set; }

        [Required]
        public string UsuarioId { get; set; }

        [Required]
        public string EspacioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
