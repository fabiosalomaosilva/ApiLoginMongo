using ApiLoginMongo.Data;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using MongoDB.Driver;

namespace ApiLoginMongo.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public IMongoCollection<RefreshToken> context { get; }
        public RefreshTokenRepository(MongoContext mongoContext)
        {
            context = mongoContext.Context.GetCollection<RefreshToken>("RefreshTokens");
        }
        public async Task Save(RefreshToken token)
        {
            await context.InsertOneAsync(token);
        }

        public async Task<string> GetByEmail(string email)
        {
            return (await context.Find(p => p.Email == email).FirstOrDefaultAsync()).Token;
        }

        public async Task<bool> ExistsToken(string token)
        {
            var exists = await context.Find(p => p.Token == token).FirstOrDefaultAsync();
            return exists != null;
        }

        public async Task Delete(string token)
        {
            var exists = await context.Find(p => p.Token == token).FirstOrDefaultAsync();
            await context.DeleteOneAsync(p => p.Token == token);
        }
    }
}
