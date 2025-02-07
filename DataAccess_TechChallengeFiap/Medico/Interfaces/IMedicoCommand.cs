using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;

namespace DataAccess_TechChallengeFiap.Medico.Interfaces
{
    public interface IMedicoCommand
    {
        Task<List<MedicoEntity>> GetMedicos();
        Task<MedicoEntity> GetMedico(int id);
        Task<MedicoEntity> GetMedicoPorNome(string nome);
        Task<MedicoEntity> GetMedicoPorCRM(string crm);
        Task<int> InsertMedico (MedicoEntity medico);
        Task<bool> UpdateMedico (int id, MedicoRepository medico);
        Task<bool> DeleteMedico (int id);
    }
}
