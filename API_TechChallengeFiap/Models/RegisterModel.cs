using System.ComponentModel.DataAnnotations;

namespace API_TechChallengeFiap.Models
{
    public class RegisterModel
    {
        public required string Email { get; set; }

        public required string Senha { get; set; }

        public required string Nome { get; set; }

        public required string CPF { get; set; }

        public string? CRM { get; set; }
        public string? Especializacao { get; set; }

        public bool IsMedico { get; set; }

    }
}
