using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdenesCompra.JPazos.domain.Entities;

namespace OrdenesCompra.JPazos.infrastructure.Configurations
{
    internal class OrdenConfiguration : IEntityTypeConfiguration<Orden>
    {
        public void Configure(EntityTypeBuilder<Orden> builder)
        {
            builder.ToTable("Ordenes");
            
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            builder.Property(e => e.Cliente)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.Total)
                .HasColumnType("decimal(18,2)");

            // Configuración de la relación uno a muchos con OrdenDetalle
            builder.HasMany(o => o.OrdenDetalle) 
                   .WithOne(od => od.Orden) 
                   .HasForeignKey(od => od.OrdenId) 
                   .OnDelete(DeleteBehavior.Cascade) 
                   .HasConstraintName("FK_Orden_OrdenDetalles");

            // Índice único para evitar duplicados
            builder.HasIndex(e => new { e.Cliente, e.FechaCreacion })
                .IsUnique()
                .HasDatabaseName("IX_Ordenes_Cliente_Fecha");
        }
    }
}
