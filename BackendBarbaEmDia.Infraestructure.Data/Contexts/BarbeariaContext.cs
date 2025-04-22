using BackendBarbaEmDia.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace BackendBarbaEmDia.Infraestructure.Data.Contexts
{
    public class BarbeariaContext : DbContext
    {
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Barbeiro> Barbeiros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<BarbeiroServico> BarbeiroServicos { get; set; }
        public DbSet<Travamento> Travamentos { get; set; }

        public BarbeariaContext(DbContextOptions<BarbeariaContext> options) : base(options) 
        {

#if RELEASE
            Database.Migrate();
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Cliente)
                .WithMany(c => c.Agendamentos)
                .HasForeignKey(a => a.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Barbeiro)
                .WithMany(b => b.Agendamentos)
                .HasForeignKey(a => a.IdBarbeiro)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Servico)
                .WithMany(s => s.Agendamentos)
                .HasForeignKey(a => a.IdServico)
                .OnDelete(DeleteBehavior.Restrict);

            // BarbeiroServico - chave composta
            modelBuilder.Entity<BarbeiroServico>()
                .HasKey(bs => new { bs.IdBarbeiro, bs.IdServico });

            modelBuilder.Entity<BarbeiroServico>()
                .HasOne(bs => bs.Barbeiro)
                .WithMany(b => b.BarbeiroServicos)
                .HasForeignKey(bs => bs.IdBarbeiro);

            modelBuilder.Entity<BarbeiroServico>()
                .HasOne(bs => bs.Servico)
                .WithMany(s => s.BarbeiroServicos)
                .HasForeignKey(bs => bs.IdServico);

            // Travamento
            modelBuilder.Entity<Travamento>()
                .HasOne(t => t.Barbeiro)
                .WithMany(b => b.Travamentos)
                .HasForeignKey(t => t.IdBarbeiro)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
