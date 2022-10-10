using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using ApiLoginMongo.Services.Interfaces;

namespace ApiLoginMongo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<User> Register(RegisterDto register)
        {
            return await _authRepository.Register(register);
        }

        public async Task<StatusLogin> Login(LoginDto login)
        {
            var responseUser = await _authRepository.Login(login);
            if (responseUser.StatusLoginResult == StatusLoginResult.Success)
            {
                responseUser.ResponseUser.Token = await _tokenService.GenerateTokenAsync(responseUser.ResponseUser);
            }
            return responseUser;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _authRepository.ChangePassword(changePasswordDto);
        }

        public async Task<bool> ResetPassword(string email)
        {
            return await _authRepository.ResetPassword(email);
        }
    }
}

