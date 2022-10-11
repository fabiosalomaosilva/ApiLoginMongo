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
                return new StatusLogin { ResponseUser = null, StatusLoginResult = StatusLoginResult.UserNotFound };
            }
            if (!user.EmailValidated)
            {
                return new StatusLogin { ResponseUser = null, StatusLoginResult = StatusLoginResult.EmailNoValidated };
            }
            if (!user.Active)
            {
                return new StatusLogin { ResponseUser = null, StatusLoginResult = StatusLoginResult.UserInactive };
            }
            var password = login.Password.ToEncript();
            if (user != null && user.Password == password && user.EmailValidated)
            {
                return new StatusLogin
                {
                    ResponseUser = new ResponseUserDto
                    {
                        Email = login.Email,
                        Role = user.Role,
                        Name = user.Name,
                        CellphoneNumber = user.CellphoneNumber,
                        RoleId = user.RoleId,
                    },
                    StatusLoginResult = StatusLoginResult.Success
                };
            }

            return new StatusLogin { ResponseUser = null, StatusLoginResult = StatusLoginResult.ErrorLogin };
        }

        public async Task<ResponseRegisterDto> Register(RegisterDto register)
        {
            try
            {
                context.Indexes.CreateOne(
                  new CreateIndexModel<User>(Builders<User>.IndexKeys.Descending(model => model.Email),
                  new CreateIndexOptions { Unique = true }));
                var user = new User
                {
                    Active = true,
                    Email = register.Email,
                    EmailValidated = true,
                    Password = register.Password.ToEncript(),
                    Name = register.Name,
                    CellphoneNumber = register.CellphoneNumber,
                    Role = RoleTypes.Admin,
                    RoleId = "1",
                    PhotoUrl = ""
                };
                await context.InsertOneAsync(user);
                var responseUser = new ResponseRegisterModelDto
                {
                    Email = register.Email,
                    Name = register.Name,
                    CellphoneNumber = register.CellphoneNumber,
                    Role = RoleTypes.Admin
                };
                return new ResponseRegisterDto
                {
                    Model = responseUser,
                    Message = "Cadastro realizado com sucesso.",
                    ResponseRegisterStatus = ResponseRegisterStatus.Success
                };
            }
            catch (MongoWriteException ex)
            {
                return new ResponseRegisterDto
                {
                    Model = null,
                    Message = "O email informado já está cadastrado na base de dados. Por favor verifique os dados.",
                    ResponseRegisterStatus = ResponseRegisterStatus.EmailDuplicado
                };
            }
            catch (Exception ex)
            {
                return new ResponseRegisterDto
                {
                    Model = null,
                    Message = ex.Message,
                    ResponseRegisterStatus = ResponseRegisterStatus.ErroServidor
                };
            }
        }

        public Task<bool> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

    }
}
