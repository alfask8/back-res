using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class EstadoReserva
    {
        [Key]
        public int IdEstado { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;  // Ejemplo: "Pendiente", "Confirmada", "Cancelada"

        // Relación 1:N → un estado puede aplicarse a muchas reservas o historiales
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();   // Relación con la tabla Reserva    
        public ICollection<HistorialReserva> Historiales { get; set; } = new List<HistorialReserva>();  //  Relación con la tabla HistorialReserva  
    }
}
