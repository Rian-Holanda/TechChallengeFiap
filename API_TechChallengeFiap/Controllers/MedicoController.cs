using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
using Infrastructure_FiapTechChallenge;
using Microsoft.AspNetCore.Mvc;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoQueries medicoQueries;
        private readonly IMedicoCommand medicoCommand;

        public MedicoController(IAppDbContext context, IMedicoQueries medicoQueries, IMedicoCommand medicoCommand)        {
           
            this.medicoQueries = medicoQueries;
            this.medicoCommand = medicoCommand;
        }


        [HttpGet("todos")]
        public async Task<IActionResult> GetMedicosAsync()        
        {

            var medicos = await medicoQueries.GetMedicos();

            return Ok(medicos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteAsync(int id)
        {

            var medicos = await medicoQueries.GetMedico(id);

            return Ok(medicos);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync(int id, [FromBody] MedicoRepository value)
        {
            var medico = await medicoCommand.UpdateMedico(id, value);
            return Ok(medico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var medico = await medicoCommand.DeleteMedico(id);
            return Ok(medico);
        }
    }
}
