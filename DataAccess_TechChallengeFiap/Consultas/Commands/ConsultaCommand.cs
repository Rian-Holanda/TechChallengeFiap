using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Consultas.Queries;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Consultas.Commands
{
    public class ConsultaCommand : IConsultaCommand
    {
        private readonly IAppDbContext context;
        private readonly ILogger<ConsultaCommand>? logger;
        public ConsultaCommand(IAppDbContext context, ILogger<ConsultaCommand>? logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<int> InsertConsulta(ConsultaEntity consulta, HistoricoConsultasEntity historicoConsulta, HorarioDiaEntity horarioDia)
        {
            try 
            { 
                var idConsulta = await context.Consultas.Add(consulta).Context.SaveChangesAsync();

                if (idConsulta > 0) 
                {
                    var idHistoricoConsulta = await context.HistoricoConsultas.Add(historicoConsulta).Context.SaveChangesAsync();
                }

                return idConsulta;
            }
            catch 
            { 
                return 0;
            }
        
        }
        public async Task<bool> UpdateConsulta(ConsultaEntity consulta, HistoricoConsultasEntity historicoConsulta, HorarioDiaEntity horarioDia)
        {
            try 
            {
                bool result = false;

                var resultConsulta = await context.Consultas.Where(c => c.Id == consulta.Id)
                                                            .ExecuteUpdateAsync(setters => setters
                                                                    .SetProperty(c => c.IdMedico, consulta.IdMedico)
                                                                    .SetProperty(c => c.IdPaciente, consulta.IdPaciente)
                                                                    .SetProperty(c => c.DataConsulta, consulta.DataConsulta));
                
                if(resultConsulta != 0) 
                {
                    var resultHistorico = await context.HistoricoConsultas.Where(c => c.Id == consulta.Id)
                                                                          .ExecuteUpdateAsync(setters => setters
                                                                            .SetProperty(c => c.IdConsuta, consulta.Id)
                                                                            .SetProperty(c => c.IdHorarioDia, historicoConsulta.IdHorarioDia));
                    
                    
                    result = (resultHistorico != 0);
                }

                return result;
            }
            catch 
            {
                return false;
            }
        }
        public async Task<bool> DeleteConsulta(int idConsulta, int idHistoricoConsulta)
        {
            try 
            {
                bool result = false;

                var resultHistorico = (idConsulta > 0) ? await context.HistoricoConsultas
                                                                      .Where(h => h.Id == idHistoricoConsulta)
                                                                      .ExecuteDeleteAsync() : 0;

                if(resultHistorico != 0) 
                {
                    var resultConsulta = (idConsulta > 0) ? await context.Consultas.Where(p => p.Id == idConsulta).ExecuteDeleteAsync() : 0;
                    result = (resultConsulta != 0);
                }

                return result;
            }
            catch 
            {
                return false;
            }
        }
    }
}
