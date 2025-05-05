using Microsoft.EntityFrameworkCore;
using OrdenesCompra.JPazos.domain.Entities;
using OrdenesCompra.JPazos.infrastructure.Configurations;

namespace OrdenesCompra.JPazos.infrastructure.Context
{
    public partial class OrdenesCompraContext : DbContext
    {
        public OrdenesCompraContext()
        {
        }

        public OrdenesCompraContext(DbContextOptions<OrdenesCompraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Orden> Ordenes { get; set; }
        public virtual DbSet<OrdenDetalle> OrdenDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new OrdenConfiguration());
            modelBuilder.ApplyConfiguration(new OrdenDetalleConfiguration());
        }
    }
}
