using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity_TechChallengeFiap.Entities;

namespace DataAccess_TechChallengeFiap.Medico.Interfaces
{
    public interface IMedicoCommand
    {
        Task<List<MedicoEntity>> GetMedicos();
        Task<MedicoEntity> GetMedico(int id);
        Task<int> InsertMedico (MedicoEntity medico);
        Task<bool> UpdateMedico (MedicoEntity medico);
        Task<bool> DeleteMedico (int id);
    }
}
