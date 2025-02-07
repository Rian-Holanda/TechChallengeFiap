﻿using DataAccess_TechChallengeFiap.Paciente.Command;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


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

  
        public async Task<int> InsertPaciente(PacienteEntity paciente)
        {
            try
            {
                var result = await context.Pacientes.AddAsync(paciente);
                await context.SaveChangesAsync();

                return paciente.Id;
            }
            catch
            {
                return paciente.Id;
            }
        }
        public async Task<bool> UpdatePaciente(int id, PacienteRepository paciente)
        {
            try
            {
                var result = await context.Pacientes
                            .Where(m => m.Id == id)
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
