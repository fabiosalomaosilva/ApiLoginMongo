using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;

namespace ApiLoginMongo.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<ResponseRegisterDto> Register(RegisterDto register);
        Task<StatusLogin> Login(LoginDto login);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<bool> ResetPassword(string email);
    }
}
