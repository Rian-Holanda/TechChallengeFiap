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

        public IActionResult Index()
        {
            return View();
        }
    }
}
