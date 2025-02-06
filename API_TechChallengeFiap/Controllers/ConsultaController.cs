using DataAccess_TechChallengeFiap.Consultas.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaCommand _consultaCommand;
        private readonly IConsultaQueries _consultaQueries;
        
        public ConsultaController(IConsultaCommand consultaCommand, IConsultaQueries consultaQueries)
        {
           _consultaCommand = consultaCommand;
           _consultaQueries = consultaQueries;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
