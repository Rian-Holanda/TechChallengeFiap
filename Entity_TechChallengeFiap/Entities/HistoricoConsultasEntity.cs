using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_TechChallengeFiap.Entities
{
    public class HistoricoConsultasEntity
    {
        public int Id { get; set; }
        public int IdConsuta { get; set; }
        public int IdHorarioDia { get; set; }
        public ConsultaEntity? Consulta { get; set; }
        public HorarioDiaEntity? HorarioDia { get; set;}

    }
}
