using ApiLoginMongo.Dtos;
using ApiLoginMongo.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ApiLoginMongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IValidator<LoginDto> _validatorLogin;
        private readonly IValidator<RegisterDto> _validatorRegisterDto;
        private readonly IValidator<ChangePasswordDto> _changePasswordDtoValidator;

        public AuthController(IAuthService service,
            IValidator<LoginDto> validatorLogin,
            IValidator<RegisterDto> validatorRegisterDto,
            IValidator<ChangePasswordDto> ChangePasswordDtoValidator)
        {
            _service = service;
            _validatorLogin = validatorLogin;
            _validatorRegisterDto = validatorRegisterDto;
            _changePasswordDtoValidator = ChangePasswordDtoValidator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto obj)
        {
            var validation = await _validatorLogin.ValidateAsync(obj);
            if (validation.IsValid)
            {
                var result = await _service.Login(obj);
                if (result.StatusLoginResult == Data.StatusLoginResult.Success)
                {
                    return Ok(result);
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.ErrorLogin || result.StatusLoginResult == Data.StatusLoginResult.UserNotFound)
                {
                    return BadRequest("Login ou senha está errado");
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.UserInactive)
                {
                    return Forbid("Usuário está inativo");
                }
                if (result.StatusLoginResult == Data.StatusLoginResult.EmailNoValidated)
                {
                    return Forbid("E-mail não foi validado.");
                }
                return BadRequest("Erro ao processar o pedido");
            }
            return BadRequest(validation.Errors[0].ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto obj)
        {
            var validation = await _validatorRegisterDto.ValidateAsync(obj);
            if (validation.IsValid)
            {
                var result = await _service.Register(obj);
                if (result.ResponseRegisterStatus == ResponseRegisterStatus.Success)
                {
                    return Ok(result.Model);
                }
                if (result.ResponseRegisterStatus == ResponseRegisterStatus.EmailDuplicado)
                {
                    result.Message = "O email informado já está cadastrado na base de dados. Por favor verifique os dados.";
                    return BadRequest(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(validation.Errors[0].ErrorMessage);
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto obj)
        {
            var validation = await _changePasswordDtoValidator.ValidateAsync(obj);
            if (validation.IsValid)
            {
                var result = await _service.ChangePassword(obj);
                if (result)
                {
                    return Ok("Senha alterada com sucesso");
                }
                return BadRequest("Erro ao efetuar a alteração de senha. Por favor, tente novamente.");
            }
            return BadRequest(validation.Errors[0].ErrorMessage);
        }
    }
}