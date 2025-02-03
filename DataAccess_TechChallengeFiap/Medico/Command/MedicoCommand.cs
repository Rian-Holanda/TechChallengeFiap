using DataAccess_TechChallengeFiap.Medico.Interfaces;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Medico.Command
{
    public class MedicoCommand : IMedicoCommand
    {
        private readonly IAppDbContext context;
        private readonly ILogger<MedicoCommand>? logger;

        public MedicoCommand(IAppDbContext context, ILogger<MedicoCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<List<MedicoEntity>> GetMedicos()
        {
            try 
            {
                var medicos = await context.Medicos.ToListAsync();

                //var medicosEntity =  (List<MedicoEntity>) medicos.Result.ToList();

                return medicos;

            }
            catch 
            {
                return new List<MedicoEntity>();
            }
        }

        public async Task<MedicoEntity> GetMedico(int id) 
        {
            try 
            { 
                return await context.Medicos.Where(m => m.Id == id).FirstAsync();
            }
            catch 
            { 
                return new MedicoEntity();
            }
        }

        public async Task<int> InsertMedico(MedicoEntity medico)
        {
            try 
            {
                var result = await context.Medicos.AddAsync(medico);

                return medico.Id;
            }
            catch 
            {
                return medico.Id;
            }
        }
        public async Task<bool> UpdateMedico(MedicoEntity medico)
        {
            try
            {
                var result = await context.Medicos
                            .Where(m => m.Id == medico.Id)
                            .ExecuteUpdateAsync(setters => setters
                                                           .SetProperty(m => m.Nome, medico.Nome)
                                                           .SetProperty(m => m.CPF,  medico.CPF)
                                                           .SetProperty(m => m.CRM,  medico.CRM));

                return (result > 0);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMedico(int id) 
        {
            try 
            {
                var result = (id > 0) ? await context.Medicos.Where(m => m.Id == id).ExecuteDeleteAsync() : 0;

                return (result != 0);
            }
            catch 
            { 
                return false;
            }
        }
    }
}


