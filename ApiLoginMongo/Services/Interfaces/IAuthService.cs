using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;
using ApiLoginMongo.Entities;

namespace ApiLoginMongo.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDto register);
        Task<StatusLogin> Login(LoginDto login);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<bool> ResetPassword(string email);
    }
}
