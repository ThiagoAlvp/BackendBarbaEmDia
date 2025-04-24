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
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<ParametrizacaoHorarioFuncionamento> ParametrizacaoHorarioFuncionamento { get; set; }

        public BarbeariaContext(DbContextOptions<BarbeariaContext> options) : base(options) 
        {

#if RELEASE
            Database.Migrate();
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Agendamento

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

            #endregion

            #region BarbeiroServicos

            modelBuilder.Entity<BarbeiroServico>()
                .HasOne(bs => bs.Barbeiro)
                .WithMany(b => b.BarbeiroServicos)
                .HasForeignKey(bs => bs.IdBarbeiro)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BarbeiroServico>()
                .HasOne(bs => bs.Servico)
                .WithMany(s => s.BarbeiroServicos)
                .HasForeignKey(bs => bs.IdServico)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Travamento

            modelBuilder.Entity<Travamento>()
                .HasOne(t => t.Barbeiro)
                .WithMany(b => b.Travamentos)
                .HasForeignKey(t => t.IdBarbeiro)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region ParametrizacaoHorarioFuncionamento

            modelBuilder.Entity<ParametrizacaoHorarioFuncionamento>().HasData(
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 1,
                    DiaSemana = DayOfWeek.Monday,
                    HoraInicio = new TimeSpan(0, 0, 0),
                    HoraFim = new TimeSpan(0, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 2,
                    DiaSemana = DayOfWeek.Tuesday,
                    HoraInicio = new TimeSpan(8, 0, 0),
                    HoraFim = new TimeSpan(19, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 3,
                    DiaSemana = DayOfWeek.Wednesday,
                    HoraInicio = new TimeSpan(8, 0, 0),
                    HoraFim = new TimeSpan(19, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 4,
                    DiaSemana = DayOfWeek.Thursday,
                    HoraInicio = new TimeSpan(8, 0, 0),
                    HoraFim = new TimeSpan(19, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 5,
                    DiaSemana = DayOfWeek.Friday,
                    HoraInicio = new TimeSpan(8, 0, 0),
                    HoraFim = new TimeSpan(19, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 6,
                    DiaSemana = DayOfWeek.Saturday,
                    HoraInicio = new TimeSpan(8, 0, 0),
                    HoraFim = new TimeSpan(16, 0, 0)
                },
                new ParametrizacaoHorarioFuncionamento
                {
                    Id = 7,
                    DiaSemana = DayOfWeek.Sunday,
                    HoraInicio = new TimeSpan(0, 0, 0),
                    HoraFim = new TimeSpan(0, 0, 0)
                }
            );

            #endregion
        }
    }
}
