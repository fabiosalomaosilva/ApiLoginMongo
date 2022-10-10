using ApiLoginMongo.Data;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using MongoDB.Driver;

namespace ApiLoginMongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IMongoCollection<User> context { get; }

        public UserRepository(MongoContext mongoContext)
        {
            context = mongoContext.Context.GetCollection<User>("Users");
        }

        public async Task<User> GetAsync(string id)
        {
            return await context.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<SearchResult<User>> GetAllAsync(int page, int pageSize)
        {
            var list = await context.Find(p => p.Active == true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new SearchResult<User>
            {
                CurrentPage = page,
                Data = list,
                PageSize = pageSize,
                TotalRecords = list.Count()
            };
        }

        public async Task<User> AddAsync(User entity)
        {
            await context.InsertOneAsync(entity);
            return entity;
        }

        public async Task<User> UpdateAsync(string id, User entity)
        {
            await context.ReplaceOneAsync(p => p.Id == id, entity);
            return entity;
        }
        public async Task DeleteAsync(string id)
        {
            await context.DeleteOneAsync(x => x.Id == id);
        }
    }
}
