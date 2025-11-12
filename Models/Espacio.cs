using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class Espacio
    {
        [Key]
        public int IdEspacio { get; set; } //Pk autogenerada

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty; // Nombre del espacio, inicializado como cadena vacía para evitar valores nulos


        [StringLength(250)]
        public string Descripcion { get; set; }=string.Empty; // Descripción del espacio, inicializado como cadena vacía para evitar valores nulos

        [Required]
        [Range(1,5000,ErrorMessage ="La capacidad del espacio está restringida a <5000 y >0")] // Capacidad minima y máxima del espacio estableciendo un rango valido
        public int Capacidad { get; set; } // Capacidad del espacio
        public bool Disponible { get; set; } = true; // Indica si el espacio está disponible para reservas, por defecto es true para evitar errores

        // Relación 1:N → un Espacio puede tener muchas Reservas
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>(); // Inicializado para evitar valores nulos
    }
}
