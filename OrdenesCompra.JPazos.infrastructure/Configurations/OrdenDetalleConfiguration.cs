using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdenesCompra.JPazos.domain.Entities;

namespace OrdenesCompra.JPazos.infrastructure.Configurations
{
    internal class OrdenDetalleConfiguration : IEntityTypeConfiguration<OrdenDetalle>
    {
        public void Configure(EntityTypeBuilder<OrdenDetalle> builder)
        {
            builder.ToTable("OrdenDetalles");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.OrdenId)
                .IsRequired();

            builder.Property(e => e.Producto)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.Cantidad)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            builder.Ignore(e => e.Subtotal);

            // Configuración de la relación con Orden
            builder.HasOne(od => od.Orden) 
                   .WithMany(o => o.OrdenDetalle) 
                   .HasForeignKey(od => od.OrdenId) 
                   .OnDelete(DeleteBehavior.Cascade) 
                   .HasConstraintName("FK_OrdenDetalles_Orden");

            // Aplicar restricciones CHECK para que los valores sean positivos
             builder.ToTable("OrdenDetalles", t =>
            {
                t.HasCheckConstraint("CK_OrdenDetalles_Cantidad", "[Cantidad] >= 0");
                t.HasCheckConstraint("CK_OrdenDetalles_PrecioUnitario", "[PrecioUnitario] >= 0");
            });


        }
    }
}
