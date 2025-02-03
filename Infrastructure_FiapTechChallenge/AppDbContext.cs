using Entity_TechChallengeFiap.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace Infrastructure_FiapTechChallenge
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options), IAppDbContext
    {
        private readonly IConfiguration _configuration = configuration;

        public required DbSet<MedicoEntity> Medicos { get; set; }
        public required DbSet<PacienteEntity> Pacientes { get; set; }
        public required DbSet<ConsultaEntity> Consultas { get; set; }
        public required DbSet<HistoricoConsultasEntity> HistoricoConsultas { get; set; }
        public required DbSet<HorarioEntity> Horarios { get; set; }
        public required DbSet<DiaEntity> Dias { get; set; }
        public required DbSet<HorarioDiaEntity> HorariosDias { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("ConnectionString");

                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MedicoEntity>(entity =>
            {
                entity.ToTable("tb_Medico");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.CPF).IsRequired();
                entity.Property(e => e.CRM).IsRequired();
                entity.Property(e => e.UserId).IsRequired();

            });

            modelBuilder.Entity<PacienteEntity>(entity =>
            {
                entity.ToTable("tb_Paciente");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.CPF).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<ConsultaEntity>(entity => 
            {
                entity.ToTable("tb_Consulta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdMedico);
                entity.Property(e => e.IdPaciente);
                entity.Property(e => e.DataConsulta);
                entity.HasOne(e => e.Medico)
                      .WithMany(e => e.Consultas)
                      .HasForeignKey(e => e.IdMedico);
                entity.HasOne(e => e.Paciente)
                      .WithMany(e => e.Consultas)
                      .HasForeignKey(e => e.IdPaciente);
            });

            modelBuilder.Entity<HistoricoConsultasEntity>(entity => 
            {
                entity.ToTable("tb_HistoricoConsulta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdConsuta);
                entity.Property(e => e.IdHorarioDia);
                entity.HasOne(e => e.Consulta)
                      .WithOne(e => e.HistoricoConsulta)
                      .HasForeignKey<HistoricoConsultasEntity>(e => e.IdConsuta);
                entity.HasOne(e => e.HorarioDia)
                     .WithOne(e => e.HistoricoConsulta)
                     .HasForeignKey<HistoricoConsultasEntity>(e => e.IdHorarioDia);

            });

            modelBuilder.Entity<HorarioEntity>(entity => 
            {
                entity.ToTable("tb_Horario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Horario);
            });

            modelBuilder.Entity<DiaEntity>(entity => 
            {
                entity.ToTable("tb_Dia");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Dia);
            });

            modelBuilder.Entity<HorarioDiaEntity>(entity => 
            {
                entity.ToTable("tb_HorarioDia");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IdHorarioInicio);
                entity.Property(e => e.IdHorarioFim);
                entity.Property(e => e.IdDia);
                entity.HasOne(e => e.Dia)
                     .WithMany(e => e.HorariosDias)
                     .HasForeignKey(e => e.IdDia);
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
