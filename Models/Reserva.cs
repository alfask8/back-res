using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reservas.API.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }  // Cambiamos a int, FK más coherente para SQL

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public string Estado { get; set; } = "pendiente";
        public string? Observaciones { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public string DniUsuario { get; set; } = string.Empty; // Relación FK  Usuario.Dni

        [Required]
        [ForeignKey("Espacio")]
        public int IdEspacio { get; set; } // Relación FK  Espacio.IdEspacio

        public Usuario Usuario { get; set; } = null!;
        public Espacio Espacio { get; set; } = null!;
    }
}
