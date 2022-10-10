using ApiLoginMongo.Entities;

namespace ApiLoginMongo.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<string> GetByEmail(string email);
        Task<bool> ExistsToken(string token);
        Task Save(RefreshToken token);
        Task Delete(string token);
    }
}
