using ApiLoginMongo.Dtos;
using ApiLoginMongo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(obj);
                if (result.StatusLoginResult == Data.StatusLoginResult.Success)
                {
                    return Ok(result);
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.ErrorLogin || result.StatusLoginResult == Data.StatusLoginResult.UserNotFound)
                {
                    return BadRequest("Login ou senha est� errado");
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.UserInactive)
                {
                    return Forbid("Usu�rio est� inativo");
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.EmailNoValidated)
                {
                    return Forbid("E-mail n�o foi validado.");
                }
                return BadRequest("Erro ao processar o pedido");
            }
            return BadRequest("Dados do usu�rio n�o foram informados.");
        }
    }
}