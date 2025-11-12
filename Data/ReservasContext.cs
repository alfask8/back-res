using Microsoft.EntityFrameworkCore;
using Reservas.API.Models;

namespace Reservas.API.Data
{
    public class ReservasContext : DbContext
    {
      
        public ReservasContext(DbContextOptions<ReservasContext> options)
            : base(options)
        {
        }

        // DbSets (una tabla por entidad)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Espacio> Espacios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<EstadoReserva> EstadosReserva { get; set; }
        public DbSet<HistorialReserva> HistorialReservas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
    }
}
