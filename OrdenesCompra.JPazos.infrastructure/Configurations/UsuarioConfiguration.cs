using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdenesCompra.JPazos.domain.Entities;

namespace OrdenesCompra.JPazos.infrastructure.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.Property(e => e.UsuarioId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("Id");
            builder.Property(e => e.Nombres)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Nombres");
            builder.Property(e => e.Apellidos)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Apellidos");
            builder.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Email");
            builder.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("Password");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("Activo");
            builder.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FechaCreacion");

        }
    }
}
