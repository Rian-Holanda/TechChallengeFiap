using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Repository
{
    public class Consulta
    {
        public string? Medico { get; set; }
        public string? Paciente { get; set; }
        public DateTime? DataConsulta { get; set; }
        public DateTime? DataMarcacaoConsulta { get; set; }
        public string? Dia { get; set; }
        public string? Horario { get; set; }
    }
}
