using API_TechChallengeFiap.Models;
using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.Config;
using Infrastructure_FiapTechChallenge.Util.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Email = Infrastructure_FiapTechChallenge.Util.Email.Email;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMedicoQueries medicoQueries;
        private readonly IMedicoCommand medicoCommand;
        private readonly IConsultaCommand consultaCommand;
        private readonly IConsultaQueries consultaQueries;
        private readonly IPacienteCommand pacienteCommand;

        public MedicoController(UserManager<ApplicationUser> userManager, 
            IAppDbContext context, 
            IMedicoQueries medicoQueries, 
            IMedicoCommand medicoCommand, 
            IConsultaCommand consultaCommand, 
            IConsultaQueries consultaQueries, 
            IPacienteCommand pacienteCommand){
           
            this._userManager = userManager;
            this.medicoQueries = medicoQueries;
            this.medicoCommand = medicoCommand;
            this.consultaCommand = consultaCommand;
            this.consultaQueries = consultaQueries;
            this.pacienteCommand = pacienteCommand;
        }


        [HttpGet("todos")]
        public async Task<IActionResult> GetMedicosAsync()        
        {

            var medicos = await medicoQueries.GetMedicos();

            return Ok(medicos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicoAsync(int id)
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

        [HttpPost("InsertHorarioDia")]
        public async Task<IActionResult> InsertHorarioDia(int idMedico, [FromBody] HorarioDiaModel horarioDiaModel)
        {
            try 
            {
                var dia = consultaCommand.GetDiaNome(horarioDiaModel.Dia).Result;
                var horarioInicio = consultaCommand.GetHorario(horarioDiaModel.HorarioInicio).Result;
                var horarioFim = consultaCommand.GetHorario(horarioDiaModel.HorarioFim).Result;

                HorarioDiaEntity horarioDiaEntity = new HorarioDiaEntity() 
                { 
                    IdDia = dia.Id,
                    IdHorarioInicio = horarioInicio.Id,
                    IdHorarioFim = horarioFim.Id,
                    IdMedico = idMedico    
                };

                var result = await medicoCommand.InsertHorarioAgenda(horarioDiaEntity);
                return Ok();

            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateHorarioDia/id")]
        public async Task<IActionResult> UpdateHorarioDia(int id, [FromBody] HorarioDiaModel horarioDiaModel)
        {
            try
            {
                var dia = consultaCommand.GetDiaNome(horarioDiaModel.Dia).Result;
                var horarioInicio = consultaCommand.GetHorario(horarioDiaModel.HorarioInicio).Result;
                var horarioFim = consultaCommand.GetHorario(horarioDiaModel.HorarioFim).Result;

                HorarioDiaEntity horarioDiaEntity = new HorarioDiaEntity()
                {
                    Id = id,
                    IdDia = dia.Id,
                    IdHorarioInicio = horarioInicio.Id,
                    IdHorarioFim = horarioFim.Id
                };

                var result = await medicoCommand.UpdateHorarioAgenda(horarioDiaEntity);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("AprovaConsulta/id/aprovacao")]
        public async Task<IActionResult> UpdateHorarioDia(int id, bool aprovacao)
        {
            try
            {  
                Email email = new Email();

                var consulta = consultaCommand.GetConsulta(id).Result;
                consulta.ConsultaAprovada = aprovacao;
                
                var result = await consultaCommand.UpdateAprovacaoConsulta(consulta);

                if (result) 
                {
                    var paciente = pacienteCommand.GetPaciente(consulta.IdPaciente).Result;
                    var medico = medicoCommand.GetMedico(consulta.IdMedico).Result;

                    var medicoUser = _userManager.FindByIdAsync(medico.UserId.ToString()).Result;
                    var pacienteUser = _userManager.FindByIdAsync(paciente.UserId.ToString()).Result;

                    var historico = consultaCommand.GetHistoricoConsulta(consulta.Id).Result;
                    var horarioDia = consultaCommand.GetHorarioDiaId(historico.IdHorarioDia).Result;
                    var horario = consultaCommand.GetHorarioId(horarioDia.IdHorarioInicio).Result;

                    var envioEmail = email.EnviarEmailConsultaAprovada(
                        pacienteUser.Email,
                        paciente.Nome,
                        medico.Nome,
                        medicoUser.Email,
                        horario.Horario,
                        historico.DataConsulta.ToString("D")
                        );

                    if (envioEmail) 
                    {
                        return Ok();
                    }

                    else 
                    {
                        BadRequest("Erro ao enviar o e-mail");
                    }
                }

                else 
                {
                    BadRequest("Erro ao aprovar a consulta");
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
