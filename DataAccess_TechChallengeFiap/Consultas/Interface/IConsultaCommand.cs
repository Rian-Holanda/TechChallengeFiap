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
        
        public Task<List<HorarioDiaEntity>> GetHorariosDias();
    }
}
