using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Consultas.Queries;
using DataAccess_TechChallengeFiap.Repository;
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
                    historicoConsulta.IdConsuta = consulta.Id;
                    var idHistoricoConsulta = await context.HistoricoConsultas.Add(historicoConsulta).Context.SaveChangesAsync();
                }

                return consulta.Id;
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
                                                                    .SetProperty(c => c.DataMarcacaoConsulta, consulta.DataMarcacaoConsulta));
                
                if(resultConsulta != 0) 
                {
                    var resultHistorico = await context.HistoricoConsultas.Where(c => c.Id == historicoConsulta.Id)
                                                                          .ExecuteUpdateAsync(setters => setters
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

        public async Task<bool> UpdateAprovacaoConsulta(ConsultaEntity consulta) 
        {
            try 
            {
                var resultConsulta = await context.Consultas.Where(c => c.Id == consulta.Id)
                                                            .ExecuteUpdateAsync(setters => setters
                                                                    .SetProperty(c => c.IdMedico, consulta.IdMedico)
                                                                    .SetProperty(c => c.IdPaciente, consulta.IdPaciente)
                                                                    .SetProperty(c => c.DataMarcacaoConsulta, consulta.DataMarcacaoConsulta)
                                                                    .SetProperty(c => c.ConsultaAprovada, consulta.ConsultaAprovada));

                return (resultConsulta > 0);
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

        public async Task<HorarioEntity> GetHorario(string horario) 
        {
            try 
            { 
                return await context.Horarios.Where(h => h.Horario == horario).FirstAsync();
            }
            catch 
            { 
                return new HorarioEntity();
            }
        }

        public async Task<HorarioDiaEntity> GetHorarioDia(int idHorarioInicio, int idDia)
        {
            try
            {
                return await context.HorariosDias.Where(h => h.IdHorarioInicio == idHorarioInicio && h.IdDia == idDia).FirstAsync();
            }
            catch
            {
                return new HorarioDiaEntity();
            }
        }

        public async Task<HorarioDiaEntity> GetHorarioDiaId(int idHorarioDia)
        {
            try 
            {
                return await context.HorariosDias.Where(h => h.Id == idHorarioDia).FirstAsync();
            }
            catch { return new HorarioDiaEntity();}
        
        }

        public async Task<HorarioEntity> GetHorarioId(int idHorario)
        {
            try
            {
                return await context.Horarios.Where(h => h.Id == idHorario).FirstAsync();
            }
            catch { return new HorarioEntity(); }

        }

        public async Task<ConsultaEntity> GetConsulta(int idConsulta) 
        {
            try
            {
                return await context.Consultas.Where(c => c.Id == idConsulta).FirstAsync();
            }
            catch
            {
                return new ConsultaEntity();
            }
        }

        public async Task<HistoricoConsultasEntity> GetHistoricoConsulta(int idConsulta) 
        {
            try
            {
                return await context.HistoricoConsultas.Where(c => c.IdConsuta == idConsulta).FirstAsync();
            }
            catch
            {
                return new HistoricoConsultasEntity();
            }
        }

        public async Task<DiaEntity> GetDia(int idDia) 
        {

            try
            {
                return await context.Dias.Where(d => d.Id == idDia).FirstAsync();
            }
            catch
            {
                return new DiaEntity();
            }
        }

        public async Task<DiaEntity> GetDiaNome(string dia)
        {

            try
            {
                return await context.Dias.Where(d => d.Dia == dia).FirstAsync();
            }
            catch
            {
                return new DiaEntity();
            }
        }
    }
}
