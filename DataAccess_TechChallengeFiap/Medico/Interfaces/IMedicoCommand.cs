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
        Task<int> InsertMedico (MedicoEntity medico);
        Task<bool> UpdateMedico (int id, MedicoRepository medico);
        Task<bool> DeleteMedico (int id);
    }
}
