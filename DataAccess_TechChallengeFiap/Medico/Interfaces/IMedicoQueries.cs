using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Medico.Interfaces
{
    public interface IMedicoQueries
    {
        Task<List<MedicoEntity>> GetMedicos();
        Task<MedicoEntity> GetMedico(int id);
    }
}
