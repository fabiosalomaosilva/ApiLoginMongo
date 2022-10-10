using ApiLoginMongo.Data;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using ApiLoginMongo.Services.Interfaces;

namespace ApiLoginMongo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            entity.Password = entity.Password.ToEncript();
            entity.Active = true;
            entity.EmailValidated = true;
            return await userRepository.AddAsync(entity);
        }
        public async Task<User> UpdateAsync(string id, User entity)
        {
            entity.Password = entity.Password.ToEncript();
            entity.Active = true;
            entity.EmailValidated = true;
            return await userRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<SearchResult<User>> GetAllAsync(int page, int pageSize)
        {
            return await userRepository.GetAllAsync(page, pageSize);
        }

        public async Task<User> GetAsync(string id)
        {
            return await userRepository.GetAsync(id);
        }

    }
}

