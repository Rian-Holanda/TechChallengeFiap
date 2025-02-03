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
    }
}


