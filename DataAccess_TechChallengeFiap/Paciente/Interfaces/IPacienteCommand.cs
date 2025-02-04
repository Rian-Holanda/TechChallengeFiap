using Entity_TechChallengeFiap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Paciente.Interfaces
{
    public interface IPacienteCommand
    {
        Task<List<PacienteEntity>> GetPacientes();
        Task<PacienteEntity> GetPaciente(int id);
        Task<int> InsertPaciente(PacienteEntity paciente);
        Task<bool> UpdatePaciente(PacienteEntity paciente);
        Task<bool> DeletePaciente(int id);
    }
}
