using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebApplication1.Models;

namespace VeterinaryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Medicamento>()
                .HasOne(m => m.Proveedor)
                .WithMany(p => p.Medicamentos)
                .HasForeignKey(m => m.ProveedorId);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne(m => m.Medicamento)
                .WithMany()
                .HasForeignKey(m => m.MedicamentoId);
        }
    }
}
