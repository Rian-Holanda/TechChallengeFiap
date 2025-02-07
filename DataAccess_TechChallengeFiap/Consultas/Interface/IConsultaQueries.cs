using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Consultas.Interface
{
    public interface IConsultaQueries
    {
        public List<ListaHorarioDias> GetHorariosDias();
        public List<ConsultasMedico> GetConsultasDisponiveisMedico(int idMedico, DateTime dataConsulta);
        public List<Consulta> GetHorariosConsultas();
        public List<Consulta> GetConsultasMedico(int idMedico);
        public List<Consulta> GetConsultasPaciente(int idPaciente);

    }
}
