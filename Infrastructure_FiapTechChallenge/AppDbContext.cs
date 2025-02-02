using Entity_TechChallengeFiap.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure_FiapTechChallenge
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : DbContext(options), IAppDbContext
    {
        private readonly IConfiguration _configuration = configuration;

        public required DbSet<MedicoEntity> Medicos { get; set; }
        public required DbSet<PacienteEntity> Pacientes { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }

}
