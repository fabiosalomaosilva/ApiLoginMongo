using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;
using ApiLoginMongo.Entities;

namespace ApiLoginMongo.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> Register(RegisterDto register);
        Task<StatusLogin> Login(LoginDto login);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<bool> ResetPassword(string email);
    }
}
