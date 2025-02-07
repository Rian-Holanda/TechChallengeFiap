using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_TechChallengeFiap.Entities
{
    public class ConsultaEntity
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime DataConsulta { get; set; }
        public MedicoEntity? Medico { get; set; }
        public PacienteEntity? Paciente { get; set; }
        public HistoricoConsultasEntity? HistoricoConsulta { get; set; }
        
    }
}
