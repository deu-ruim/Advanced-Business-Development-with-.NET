using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GB1.Domain.Entitiy;

namespace GB1.Infrastructure.Mappings
{
    public class DesastreMapping : IEntityTypeConfiguration<Desastre>
    {
        public void Configure(EntityTypeBuilder<Desastre> builder)
        {
            builder
                .ToTable("DESASTRE");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .HasColumnName("ID")
                .HasDefaultValueSql("DESASTRE_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(d => d.Uf)
                .IsRequired()
                .HasColumnName("UF")
                .HasMaxLength(2);

            builder
                .Property(d => d.Titulo)
                .HasColumnName("TITULO")
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(d => d.Descricao)
                .HasColumnName("DESCRICAO")
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(d => d.DataDesastre)
                .HasColumnName("DATA_DESASTRE")
                .IsRequired();

            builder
                .Property(d => d.Severidade)
                .HasColumnName("SEVERIDADE")
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(d => d.UsuarioId);

            builder
                .HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
