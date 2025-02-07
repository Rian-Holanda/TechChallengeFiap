using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Medico.Queries
{
    public class MedicoQueries : IMedicoQueries
    {
        private readonly IAppDbContext _context;

        public MedicoQueries(IAppDbContext context)
        {
                this._context = context;
        }

        public async Task<List<MedicoEntity>> GetMedicos()
        {
            try
            {
                var medicos = await _context.Medicos.ToListAsync();

                return medicos;

            }
            catch
            {
                return new List<MedicoEntity>();
            }
        }

        public async Task<MedicoEntity> GetMedico(int id)
        {
            try
            {
                return await _context.Medicos.Where(m => m.Id == id).FirstAsync();
            }
            catch
            {
                return new MedicoEntity();
            }
        }
    }
}
