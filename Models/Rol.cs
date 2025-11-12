using System.ComponentModel.DataAnnotations;

namespace Reservas.API.Models
{
    public class Rol
    {
        [Key]
        public required int IdRol { get; set; } //Pk autogenerada 

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; } = string.Empty;  // Nombre del rol (e.g., "Admin", "Usuario"), inicializado como cadena vacía para evitar valores nulos



        public ICollection<Usuario>Usuarios { get; set; } = new List<Usuario>(); // Relación uno a muchos con Usuarios
    }
}
