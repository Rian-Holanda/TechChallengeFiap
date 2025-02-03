using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_TechChallengeFiap.Entities
{
    public class HorarioDiaEntity
    {
        public int Id { get; set; }
        public int IdHorarioInicio { get; set; }
        public int IdHorarioFim {  get; set; }
        public int IdDia { get; set; }
        public HistoricoConsultasEntity? HistoricoConsulta { get; set; }
        public DiaEntity? Dia { get; set; }

    }
}
