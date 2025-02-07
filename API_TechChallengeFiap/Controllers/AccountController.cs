using API_TechChallengeFiap.Models;
using DataAccess_TechChallengeFiap.Medico.Interfaces;
using DataAccess_TechChallengeFiap.Paciente.Interfaces;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_TechChallengeFiap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppDbContext _context;
        private readonly IMedicoCommand _medicoCommand;
        private readonly IPacienteCommand _pacienteCommand;

        public AccountController(UserManager<ApplicationUser> userManager, IAppDbContext _context, RoleManager<IdentityRole> roleManager, IMedicoCommand medicoCommand, IPacienteCommand pacienteCommand)
        {
            this._userManager = userManager;
            this._context = _context;
            this._roleManager = roleManager;
            this._medicoCommand = medicoCommand;
            this._pacienteCommand = pacienteCommand;
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
            int retorno = 0;

            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Este e-mail já está em uso.");
                return BadRequest(ModelState);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
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

                        retorno = await _medicoCommand.InsertMedico(medico);
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

                        retorno = await _pacienteCommand.InsertPaciente(paciente);

                    }

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return Ok(new { Return = retorno, Message = "Usuário registrado com sucesso!" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, ex.Message);
            }
           
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

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuário atualizado com sucesso!" });
        }

    }
}
