using System;
using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class HistorialReserva
    {
        [Key]
        public int IdHistorial { get; set; } // Clave primaria

        [Required]
        public int IdReserva { get; set; }// Clave foránea a Reserva

        [Required]
        public int IdEstado { get; set; }// Clave foránea a EstadoReserva

        public DateTime FechaCambio { get; set; } = DateTime.UtcNow; // Fecha del cambio de estado estableciendo el valor actual por defecto    

        [StringLength(500)]
        public string Observaciones { get; set; } = string.Empty; // Observaciones sobre el cambio de estado

        // Relaciones
        public Reserva Reserva { get; set; } = null!; // Navegación a Reserva
        public EstadoReserva Estado { get; set; } = null!; // Navegación a EstadoReserva
    }
}
