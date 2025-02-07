using DataAccess_TechChallengeFiap.Paciente.Command;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Paciente.Queries
{
    public class PacienteQueries : IPacienteQueries
    {
        private readonly IAppDbContext context;        

        public PacienteQueries(IAppDbContext context, ILogger<PacienteQueries>? logger)
        {
            this.context = context;           
        }

        public async Task<List<PacienteEntity>> GetPacientes()
        {
            try
            {
                var pacientes = await context.Pacientes.ToListAsync();
               

                return pacientes;

            }
            catch
            {
                return new List<PacienteEntity>();
            }
        }
        public async Task<PacienteEntity> GetPaciente(int id)
        {
            try
            {
                return await context.Pacientes.Where(m => m.Id == id).FirstAsync();
            }
            catch
            {
                return new PacienteEntity();
            }
        }
    }
}
