using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;

namespace ApiLoginMongo.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseRegisterDto> Register(RegisterDto register);
        Task<StatusLogin> Login(LoginDto login);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<bool> ResetPassword(string email);
    }
}
