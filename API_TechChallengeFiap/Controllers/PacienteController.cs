using API_TechChallengeFiap.Models;
using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Medico.Interfaces;
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
        private readonly IConsultaCommand _consultaCommand;
        private readonly IMedicoQueries _medicoQueries;
        public PacienteController(IPacienteCommand pacienteCommand, IPacienteQueries pacienteQueries, IConsultaCommand consultaCommand, IMedicoQueries medicoQueries)
        {
            this._pacienteCommand = pacienteCommand;
            this._pacienteQueries = pacienteQueries;
            this._consultaCommand = consultaCommand;
            this._medicoQueries = medicoQueries;
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

        [HttpGet("GetMedicoPorEspecializacao/especializacao")]
        public async Task<IActionResult> GetMedicoPorEspecializacao(string especializacao)
        {
            var medicos = await _medicoQueries.GetMedicoPorEspecializacao(especializacao);

            return Ok(medicos);
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

        [HttpDelete("CancelaConsulta/id")]
        public async Task<IActionResult> CancelaConsulta(int id)
        {
            var historico = _consultaCommand.GetHistoricoConsulta(id).Result;
            var consulta = await _consultaCommand.DeleteConsulta(id, historico.Id);

            if (consulta) 
            {
                return Ok("Consulta cancelada");
            }
            else 
            {
                return BadRequest();
            }
            
        }
    }
}
