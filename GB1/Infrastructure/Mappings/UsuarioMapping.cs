using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GB1.Domain.Entitiy;

namespace GB1.Infrastructure.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .ToTable("USUARIO");


            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("USUARIO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasColumnName("EMAIL")
                .HasMaxLength(255);

            builder
                .Property(u => u.Username)
                .HasColumnName("USERNAME")
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(u => u.Senha)
                .HasColumnName("SENHA")
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(u => u.Uf)
                .HasColumnName("UF")
                .IsRequired()
                .HasMaxLength(2);

            builder
                .Property(u => u.Nivel)
                .HasColumnName("NIVEL")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
