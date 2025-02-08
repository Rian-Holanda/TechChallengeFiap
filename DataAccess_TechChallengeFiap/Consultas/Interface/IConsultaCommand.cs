using Entity_TechChallengeFiap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Consultas.Interface
{
    public interface IConsultaCommand
    {
        public Task<int> InsertConsulta(ConsultaEntity consulta, HistoricoConsultasEntity historicoConsulta, HorarioDiaEntity horarioDia);
        public Task<bool> UpdateConsulta(ConsultaEntity consulta, HistoricoConsultasEntity historicoConsulta, HorarioDiaEntity horarioDia);
        public Task<bool> UpdateAprovacaoConsulta(ConsultaEntity consulta);
        public Task<bool> DeleteConsulta(int idConsulta, int idHistoricoConsulta);
        public Task<HorarioEntity> GetHorario(string horario);
        public Task<HorarioDiaEntity> GetHorarioDia(int idHorarioInicio, int idDia);
        public Task<HorarioDiaEntity> GetHorarioDiaId(int idHorarioDia);
        public Task<HorarioEntity> GetHorarioId(int idHorario);
        public Task<ConsultaEntity> GetConsulta(int idConsulta);
        public Task<HistoricoConsultasEntity> GetHistoricoConsulta(int idConsulta);
        public Task<DiaEntity> GetDia(int idDia);
        public Task<DiaEntity> GetDiaNome(string dia);


    }
}
