using Microsoft.EntityFrameworkCore;
using Reservas.Api.Models;

namespace Reservas.Api.Data
{
    public class ReservasContext : DbContext
    {
        public ReservasContext(DbContextOptions<ReservasContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuarios
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Dni);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Reservas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId);

            // Reservas
            modelBuilder.Entity<Reserva>()
                .HasKey(r => r.Id);
        }
    }
}