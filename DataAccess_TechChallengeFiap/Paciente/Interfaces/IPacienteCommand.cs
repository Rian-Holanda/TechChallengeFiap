
using Entity_TechChallengeFiap.Entities;
using DataAccess_TechChallengeFiap.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Paciente.Interfaces
{
    public interface IPacienteCommand
    {
        Task<int> InsertPaciente(PacienteEntity paciente);
        Task<bool> UpdatePaciente(int id, PacienteRepository paciente);
        Task<bool> DeletePaciente(int id);
    }
}
