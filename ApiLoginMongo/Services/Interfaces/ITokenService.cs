using ApiLoginMongo.Dtos;
using System.Security.Claims;

namespace ApiLoginMongo.Services.Interfaces
{
    public interface ITokenService
    {
        Task<TokenDto> GenerateTokenAsync(ResponseUserDto user);
        ClaimsPrincipal GetClaimsPrincipal(string token);
        Task<TokenDto> RegenerateTokenAsync(IEnumerable<Claim> claims, string refreshToken);
    }
}
