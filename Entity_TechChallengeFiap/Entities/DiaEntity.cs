using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_TechChallengeFiap.Entities
{
    public class DiaEntity
    {
        public int Id { get; set; }
        public string? Dia {  get; set; }
        public List<HorarioDiaEntity>? HorariosDias { get; set; }

    }
}
