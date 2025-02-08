using Entity_TechChallengeFiap.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_FiapTechChallenge
{
    public interface IAppDbContext
    {
        DatabaseFacade Database { get; }
        public DbSet<MedicoEntity> Medicos { get; set; }
        public DbSet<PacienteEntity> Pacientes { get; set; }
        public DbSet<ConsultaEntity> Consultas { get; set; }
        public DbSet<HistoricoConsultasEntity> HistoricoConsultas { get; set; }
        public DbSet<HorarioEntity> Horarios { get; set; }
        public DbSet<DiaEntity> Dias { get; set; }
        public DbSet<HorarioDiaEntity> HorariosDias { get; set; }
        int SaveChanges();       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
