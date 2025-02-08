using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_TechChallengeFiap.Entities
{
    public class MedicoEntity
    {
        public int Id { get; set; }
        public string? Nome { get; set; } 
        public string? CPF { get; set; } 
        public string? CRM { get; set; } 
        public string? Especializacao { get; set; }
        public Guid UserId { get; set; }
        public List<ConsultaEntity>? Consultas { get; set; }

    }
}
