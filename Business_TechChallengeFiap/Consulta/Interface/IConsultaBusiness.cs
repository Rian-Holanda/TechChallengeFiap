using DataAccess_TechChallengeFiap.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengeFiap.Consulta.Interface
{
    public interface IConsultaBusiness
    {
        public bool ValidaConsultaDisponivel(string dia, string horario, List<ConsultasMedico> consultasMedico);
    }
}
