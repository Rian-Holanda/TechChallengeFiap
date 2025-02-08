using Business_TechChallengeFiap.Consulta.Interface;
using DataAccess_TechChallengeFiap.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TechChallengeFiap.Consulta.Domain
{
    public class ConsultaBusiness: IConsultaBusiness
    {
        

        public bool ValidaConsultaDisponivel(string dia, string horario, List<ConsultasMedico>consultasMedico)
        {
            try 
            { 
                var result = consultasMedico
                    .Where(consultaMedico => consultaMedico.Dia == dia && consultaMedico.Horario == horario)
                    .ToList();

                return (result.Count == 1);
            }
            catch 
            {
                return false;
            }
        }
    }
}
