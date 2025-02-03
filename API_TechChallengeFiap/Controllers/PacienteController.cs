using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IAppDbContext _context;

        public PacienteController(IAppDbContext context)
        {
            this._context = context;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<IEnumerable<PacienteEntity>>> GetPacientesAsync()
        {

            var pacientes = await _context.Pacientes.ToListAsync();

            return Ok(pacientes);
        }
    }
}
