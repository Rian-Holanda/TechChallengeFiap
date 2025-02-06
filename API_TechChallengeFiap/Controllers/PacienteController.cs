using API_TechChallengeFiap.Models;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
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

        private readonly IPacienteCommand _pacienteCommand;
        private readonly IPacienteQueries _pacienteQueries;

        public PacienteController(IPacienteCommand pacienteCommand, IPacienteQueries pacienteQueries)
        {
            this._pacienteCommand = pacienteCommand;
            this._pacienteQueries = pacienteQueries;
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetPacientesAsync()
        {
            var pacientes = await _pacienteQueries.GetPacientes();

            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteAsync(int id)
        {
            var pacientes = await _pacienteQueries.GetPaciente(id);

            return Ok(pacientes);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync(int id, [FromBody] PacienteRepository value)
        {
            var paciente = await _pacienteCommand.UpdatePaciente(id, value);
            return Ok(paciente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var paciente = await _pacienteCommand.DeletePaciente(id);
            return Ok(paciente);
        }
    }
}
