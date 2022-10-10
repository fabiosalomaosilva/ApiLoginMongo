using ApiLoginMongo.Data;
using ApiLoginMongo.Dtos;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using MongoDB.Driver;

namespace ApiLoginMongo.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public IMongoCollection<User> context { get; }
        public AuthRepository(MongoContext mongoContext)
        {
            context = mongoContext.Context.GetCollection<User>("Users");
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await context.Find(p => p.Email == changePasswordDto.Email).FirstOrDefaultAsync();
            if (user != null && user.Password == changePasswordDto.OldPassword.ToEncript())
            {
                user.Password = changePasswordDto.Password.ToEncript();
                await context.ReplaceOneAsync(p => p.Id == user.Id, user);
                return true;
            }
            return false;
        }

        public async Task<StatusLogin> Login(LoginDto login)
        {
            var user = await context.Find(p => p.Email == login.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                return new StatusLogin { User = null, StatusLoginResult = StatusLoginResult.UserNotFound };
            }
            if (!user.EmailValidated)
            {
                return new StatusLogin { User = null, StatusLoginResult = StatusLoginResult.EmailNoValidated };
            }
            if (!user.Active)
            {
                return new StatusLogin { User = null, StatusLoginResult = StatusLoginResult.UserInactive };
            }

            if (user != null && user.Password == login.Password.ToEncript() && user.EmailValidated)
            {
                return new StatusLogin { User = user, StatusLoginResult = StatusLoginResult.Success };
            }

            return new StatusLogin { User = null, StatusLoginResult = StatusLoginResult.ErrorLogin };
        }

        public async Task<User> Register(RegisterDto register)
        {
            try
            {
                var user = new User
                {
                    Active = true,
                    Email = register.Email,
                    EmailValidated = true,
                    Password = register.Password.ToEncript(),
                    Name = register.Name,
                    CellphoneNumber = register.CellphoneNumber
                };
                await context.InsertOneAsync(user);
                return user;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public Task<bool> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

    }
}
