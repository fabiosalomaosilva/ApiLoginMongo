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

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> Register(RegisterDto register)
        {
            return await _authRepository.Register(register);
        }

        public async Task<StatusLogin> Login(LoginDto login)
        {
            return await _authRepository.Login(login);
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

