using System;
using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class Notificacion
    {
        [Key]
        public int IdNotificacion { get; set; } // Clave primaria

        [Required]
        public int IdReserva { get; set; } // Clave foránea a Reserva

        [Required]
        [StringLength(9)]
        [RegularExpression(@"^\d{8}[A-Z]$", ErrorMessage = "Formato de DNI inválido (ejemplo: 12345678A).")]
        public string DniUsuario { get; set; } = string.Empty; // DNI del usuario que recibe la notificación, también es clave foránea a Usuario

        [Required]
        [StringLength(500)]
        public string Mensaje { get; set; } = string.Empty; // Mensaje de la notificación
        
        public DateTime FechaEnvio { get; set; } = DateTime.UtcNow; // Fecha de envío de la notificación

        public bool Leida { get; set; } = false; // Indica si la notificación ha sido leída, por defecto es false

        // Relaciones
        public Reserva Reserva { get; set; } = null!; // Navegación a Reserva
        public Usuario Usuario { get; set; } = null!; // Navegación a Usuario
    }
}
