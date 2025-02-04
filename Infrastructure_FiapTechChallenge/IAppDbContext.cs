using Entity_TechChallengeFiap.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_FiapTechChallenge
{
    public interface IAppDbContext
    {
        public DbSet<MedicoEntity> Medicos { get; set; }
        public DbSet<PacienteEntity> Pacientes { get; set; }
        int SaveChanges();       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
