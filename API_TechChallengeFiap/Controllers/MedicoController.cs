using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IAppDbContext _context;

        public MedicoController(IAppDbContext context)
        {
            this._context = context;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<MedicoEntity>>> GetMedicosAsync()
        {

            var pacientes = await _context.Medicos.ToListAsync();

            return Ok(pacientes);
        }
    }
}
