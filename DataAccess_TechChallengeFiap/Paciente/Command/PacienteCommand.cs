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

namespace DataAccess_TechChallengeFiap.Paciente.Command
{
    public class PacienteCommand : IPacienteCommand
    {
        private readonly IAppDbContext context;
        private readonly ILogger<PacienteCommand>? logger;

        public PacienteCommand(IAppDbContext context, ILogger<PacienteCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<PacienteEntity>> GetPacientes()
        {
            try
            {
                var pacientes = await context.Pacientes.ToListAsync();

                //var PacientesEntity =  (List<PacienteEntity>) Pacientes.Result.ToList();

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
                return await context.Pacientes.Where(p => p.Id == id).FirstAsync();
            }
            catch
            {
                return new PacienteEntity();
            }
        }

        public async Task<PacienteEntity> GetPacientePorNome(string nome)
        {
            try
            {
                return await context.Pacientes.Where(p => p.Nome == nome).FirstAsync();
            }
            catch
            {
                return new PacienteEntity();
            }
        }

        public async Task<int> InsertPaciente(PacienteEntity paciente)
        {
            try
            {
                var result = await context.Pacientes.AddAsync(paciente);

                return paciente.Id;
            }
            catch
            {
                return paciente.Id;
            }
        }
        public async Task<bool> UpdatePaciente(PacienteEntity paciente)
        {
            try
            {
                var result = await context.Pacientes
                            .Where(m => m.Id == paciente.Id)
                            .ExecuteUpdateAsync(setters => setters
                                                           .SetProperty(p => p.Nome, paciente.Nome)
                                                           .SetProperty(p => p.CPF, paciente.CPF));

                return (result > 0);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletePaciente(int id)
        {
            try
            {
                var result = (id > 0) ? await context.Pacientes.Where(m => m.Id == id).ExecuteDeleteAsync() : 0;

                return (result != 0);
            }
            catch
            {
                return false;
            }
        }
    }
}
