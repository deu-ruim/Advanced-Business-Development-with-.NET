using GB1.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
using System;

namespace GB1.Infrastructure.Context
{
    public class DeuRuimContext : DbContext
    {
        public DeuRuimContext(DbContextOptions<DeuRuimContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Desastre> Desastres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("USUARIO_SEQ").StartsAt(10).IncrementsBy(1);
            modelBuilder.HasSequence<int>("DESASTRE_SEQ").StartsAt(10).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeuRuimContext).Assembly);
        }
    }
}
