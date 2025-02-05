using DataAccess_TechChallengeFiap.Consultas.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_TechChallengeFiap.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaCommand _consultaCommand;
        private readonly IConsultaQueries _consultaQueries;
        
        public ConsultaController(IConsultaCommand consultaCommand, IConsultaQueries consultaQueries)
        {
           _consultaCommand = consultaCommand;
           _consultaQueries = consultaQueries;
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

        [HttpGet("GetConsultasDisponiveisMedico/id")]
        public IActionResult GetConsultasDisponiveisMedico(int id)
        {
            var horariosConsultas = _consultaQueries.GetConsultasDisponiveisMedico(id);

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

        //GetConsultasDisponiveisMedico
    }
}
