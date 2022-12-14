using ApiLoginMongo.Dtos;
using ApiLoginMongo.Entities;
using ApiLoginMongo.Repositories.Interfaces;
using ApiLoginMongo.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiLoginMongo.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<TokenDto> GenerateTokenAsync(ResponseUserDto user)
        {
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var apiKey = _configuration.GetSection("ApiKey").Value;
                var key = Encoding.ASCII.GetBytes(apiKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GetClaimsIdentity(user),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var t = tokenHandler.WriteToken(token);
                var data = DateTime.Now;

                return new TokenDto
                {
                    access_token = t,
                    expires = DateTime.UtcNow.AddMinutes(60).Second.ToString(),
                    email = user.Email,
                    token_type = "bearer",
                    refreshToken = await GenerateRefreshToken(user.Email)
                };
            }
            return null;
        }
        public async Task<TokenDto> RegenerateTokenAsync(IEnumerable<Claim> claims, string refreshToken)
        {
            if (claims != null)
            {
                if (!(await _refreshTokenRepository.ExistsToken(refreshToken))) throw new SecurityTokenException("Invalid token");
                await _refreshTokenRepository.Delete(refreshToken);

                var tokenHandler = new JwtSecurityTokenHandler();
                var apiKey = _configuration.GetConnectionString("ApiKey");
                var key = Encoding.ASCII.GetBytes(apiKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GetClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var secureToken = tokenHandler.CreateToken(tokenDescriptor);
                var accessToken = tokenHandler.WriteToken(secureToken);
                var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                return new TokenDto
                {
                    access_token = accessToken,
                    expires = DateTime.UtcNow.AddMinutes(60).Second.ToString(),
                    email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                    token_type = "bearer",
                    refreshToken = await GenerateRefreshToken(email)
                };
            }
            return null;
        }
        private ClaimsIdentity GetClaimsIdentity(ResponseUserDto user)
        {
            return new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                }
            );
        }

        private ClaimsIdentity GetClaimsIdentity(IEnumerable<Claim> claims)
        {
            return new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value),
                    new Claim(ClaimTypes.Role, claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value),
                    new Claim(ClaimTypes.Name, claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value)
                }
            );
        }

        private async Task<string> GenerateRefreshToken(string email)
        {
            var random = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            var token = Convert.ToBase64String(random);
            var refreshToken = new RefreshToken
            {
                Token = token,
                Email = email
            };
            await _refreshTokenRepository.Save(refreshToken);
            return token;
        }
        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            try
            {
                var apiKey = _configuration.GetConnectionString("ApiKey");

                var tvp = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(apiKey)),
                    ValidateLifetime = false
                };
                var tokenHadler = new JwtSecurityTokenHandler();

                var principal = tokenHadler.ValidateToken(token, tvp, out var securityToken);
                return principal;
            }

            catch (SecurityTokenException)
            {
                throw new SecurityTokenException("Invalid token");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

