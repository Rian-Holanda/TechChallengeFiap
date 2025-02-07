using API_TechChallengeFiap.Models;
using Business_TechChallengeFiap.Consulta.Interface;
using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaCommand _consultaCommand;
        private readonly IConsultaQueries _consultaQueries;
        private readonly IMedicoCommand _medicoCommand;
        private readonly IPacienteCommand _pacienteCommand;
        private readonly IConsultaBusiness _consultaBusiness;


        public ConsultaController(IConsultaCommand consultaCommand, IConsultaQueries consultaQueries, IMedicoCommand medicoCommand, IPacienteCommand pacienteCommand, IConsultaBusiness consultaBusiness)
        {
            _consultaCommand = consultaCommand;
            _consultaQueries = consultaQueries;
            _medicoCommand = medicoCommand;
            _pacienteCommand = pacienteCommand;
            _consultaBusiness = consultaBusiness;
        }

        [HttpGet("GetHorariosConsultas")]
        public IActionResult GetHorariosConsultas()
        {
            var horariosConsultas = _consultaQueries.GetHorariosDias();

            if (horariosConsultas != null)
            {
                return Ok(horariosConsultas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetConsultasDisponiveisMedico/id/data")]
        public IActionResult GetConsultasDisponiveisMedico(int id, DateTime data)
        {
            var dia = data.ToString(@"dddd", new CultureInfo("PT-br")).Replace("-feira","");

            var horariosConsultas = _consultaQueries.GetConsultasDisponiveisMedico(id, data, dia);

            if (horariosConsultas != null)
            {
                return Ok(horariosConsultas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetHorariosConsultasMarcadas")]
        public IActionResult GetHorariosConsultasMarcadas()
        {
            var horariosConsultas = _consultaQueries.GetHorariosConsultas();

            if (horariosConsultas != null)
            {
                return Ok(horariosConsultas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetConsultasPaciente/id")]
        public IActionResult GetConsultasPaciente(int id)
        {
            var horariosConsultas = _consultaQueries.GetConsultasPaciente(id);

            if (horariosConsultas != null)
            {
                return Ok(horariosConsultas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetConsultasMedico/id")]
        public IActionResult GetConsultasMedico(int id)
        {
            var horariosConsultas = _consultaQueries.GetConsultasMedico(id);

            if (horariosConsultas != null)
            {
                return Ok(horariosConsultas);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("InsertConsulta")]
        public async Task<IActionResult> InsertConsulta([FromBody] ConsultaModel consultaModel)
        {
            MedicoEntity? medicoEntity = new MedicoEntity();
            PacienteEntity? pacienteEntity = new PacienteEntity();
            HorarioEntity? horarioEntity = new HorarioEntity();
            HorarioDiaEntity? horarioDiaEntity = new HorarioDiaEntity();
            DiaEntity diaEntity = new DiaEntity();

            medicoEntity = _medicoCommand.GetMedicoPorNome(consultaModel?.Medico).Result;
            pacienteEntity = _pacienteCommand.GetPacientePorNome(consultaModel.Paciente).Result;
            horarioEntity = _consultaCommand.GetHorario(consultaModel.Horario).Result;
            diaEntity = _consultaCommand.GetDiaNome(consultaModel.Dia).Result;
            horarioDiaEntity = _consultaCommand.GetHorarioDia(horarioEntity.Id, diaEntity.Id).Result;
            var consultasMedico = _consultaQueries.GetConsultasDisponiveisMedico(medicoEntity.Id, consultaModel.Data, diaEntity.Dia);

            if (_consultaBusiness.ValidaConsultaDisponivel(diaEntity.Dia, horarioEntity.Horario, consultasMedico))
            {
                ConsultaEntity consulta = new ConsultaEntity()
                {
                    IdMedico = medicoEntity.Id,
                    IdPaciente = pacienteEntity.Id,
                    DataMarcacaoConsulta = DateTime.Now

                };

                HistoricoConsultasEntity historicoConsulta = new HistoricoConsultasEntity() 
                { 
                    IdHorarioDia = horarioDiaEntity.Id,
                    DataConsulta = consultaModel.Data
                };

                var result = await _consultaCommand.InsertConsulta(consulta, historicoConsulta, horarioDiaEntity);


                if (result > 0)
                {
                    return Ok("Sucesso");
                }
                else
                {
                    return NoContent();
                }
            }

            else
            {
                return NoContent();
            }
        }


        [HttpPut("UpdateConsulta/id")]
        public async Task<IActionResult> UpdateConsulta(int id, [FromBody] ConsultaModel consultaModel)
        {
            MedicoEntity? medicoEntity = new MedicoEntity();
            PacienteEntity? pacienteEntity = new PacienteEntity();
            HorarioEntity? horarioEntity = new HorarioEntity();
            HorarioDiaEntity? horarioDiaEntity = new HorarioDiaEntity();
            DiaEntity diaEntity = new DiaEntity();

            medicoEntity = _medicoCommand.GetMedicoPorNome(consultaModel?.Medico).Result;
            pacienteEntity = _pacienteCommand.GetPacientePorNome(consultaModel.Paciente).Result;
            horarioEntity = _consultaCommand.GetHorario(consultaModel.Horario).Result;
            diaEntity = _consultaCommand.GetDiaNome(consultaModel.Dia).Result;
            horarioDiaEntity = _consultaCommand.GetHorarioDia(horarioEntity.Id, diaEntity.Id).Result;
            var consultasMedico = _consultaQueries.GetConsultasDisponiveisMedico(medicoEntity.Id, consultaModel.Data, diaEntity.Dia);

            if (_consultaBusiness.ValidaConsultaDisponivel(diaEntity.Dia, horarioEntity.Horario, consultasMedico))
            {

                ConsultaEntity consulta = new ConsultaEntity()
                {
                    Id = id,
                    IdMedico = medicoEntity.Id,
                    IdPaciente = pacienteEntity.Id,
                    DataMarcacaoConsulta = DateTime.Now

                };

                HistoricoConsultasEntity historicoConsulta = new HistoricoConsultasEntity();
                historicoConsulta = _consultaCommand.GetHistoricoConsulta(id).Result;
                historicoConsulta.IdHorarioDia = horarioDiaEntity.Id;
                historicoConsulta.DataConsulta = consultaModel.Data;

                var result = await _consultaCommand.UpdateConsulta(consulta, historicoConsulta, horarioDiaEntity);


                if (result)
                {
                    return Ok("Sucesso");
                }
                else
                {
                    return NoContent();
                }
            }
            else 
            {
                return NoContent();
            }

        }

        [HttpDelete("DeleteConsulta/id")]
        public async Task<IActionResult> DeleteConsulta(int id)
        {
            var historicoConsulta = _consultaCommand.GetHistoricoConsulta(id).Result;
            var result = await _consultaCommand.DeleteConsulta(id, historicoConsulta.Id);

            if (result)
            {
                return Ok("Sucesso");
            }
            else
            {
                return NoContent();
            }
        }
        //GetConsultasDisponiveisMedico
    }
}
