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
        public Task<DataTable> GetHorariosDias();
        public Task<DataTable> GetConsultasDisponiveisMedico(int idMedico);
        public Task<DataTable> GetHorariosConsultas();
        public Task<DataTable> GetConsultasMedico(int idMedico);
        public Task<DataTable> GetConsultasPaciente(int idPaciente);
        public Task<DataTable> GetConsultasMedicos();
        public Task<DataTable> GetConsultasPacientes();
        public Task<DataTable> GetHistoricoConsulta(int idConsulta);

    }
}
