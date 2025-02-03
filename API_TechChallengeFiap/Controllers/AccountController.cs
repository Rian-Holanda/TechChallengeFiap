using API_TechChallengeFiap.Models;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, IAppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._context = context;
            this._roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { Message = "E-mail ou senha inválidos." });
            }

   
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Senha);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "E-mail ou senha inválidos." });
            }     

            return Ok(new
            {                
                Message = "Login realizado com sucesso!"
            });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Este e-mail já está em uso.");
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email, 
                Email = model.Email,
            };
            
            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                
                if (model.IsMedico)
                {         
                    await _userManager.AddToRoleAsync(user, "Medico");

                    var medico = new MedicoEntity
                    {
                        Nome = model.Nome,
                        CRM = model.CRM,
                        CPF = model.CPF,
                        UserId = Guid.Parse(user.Id)
                    };
                    _context.Medicos.Add(medico);
                  
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Paciente");

                    var paciente = new PacienteEntity
                    {
                        Nome = model.Nome,
                        CPF = model.CPF,
                        UserId = Guid.Parse(user.Id)
                    };

                    _context.Pacientes.Add(paciente);
                   
                }

                await _context.SaveChangesAsync();

                return Ok(new { Message = "Usuário registrado com sucesso!" });
            }

           
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("create/rule")]
        public async Task<IActionResult> CreateRole([FromBody] string perfil)
        {
            if (string.IsNullOrEmpty(perfil))
            {
                return BadRequest("O nome da role é obrigatório.");
            }

            var roleExist = await _roleManager.RoleExistsAsync(perfil);
            if (roleExist)
            {
                return Conflict("A role já existe.");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(perfil));
            if (result.Succeeded)
            {
                return Ok($"Perfil {perfil} criada com sucesso.");
            }
            else
            {
                return BadRequest($"Erro ao criar perfil: {string.Join(", ", result.Errors)}");
            }
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                        
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new { Message = "Usuário não encontrado." });
            }
           
            user.UserName = model.Email;
            user.Email = model.Email;    

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(ModelState);
            }
       
            if (model.IsMedico)
            {
                var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.UserId == Guid.Parse(user.Id));
                if (medico == null)
                {
                    return NotFound(new { Message = "Médico não encontrado." });
                }

                medico.Nome = model.Nome;
                medico.CRM = model.CRM;
                medico.CPF = model.CPF;

                _context.Medicos.Update(medico);
            }
            else
            {
                var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.UserId == Guid.Parse(user.Id));
                if (paciente == null)
                {
                    return NotFound(new { Message = "Paciente não encontrado." });
                }

                paciente.Nome = model.Nome;
                paciente.CPF = model.CPF;    

                _context.Pacientes.Update(paciente);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuário atualizado com sucesso!" });
        }
    }
}
