using ChatServer.Api.Services.Interfaces;
using ChatServer.Api.ViewModels.User;
using Microsoft.IdentityModel.Tokens;
using static ChatServer.Shared.Consts.RoleConst;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatServer.Api.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private const int EXPIRED_DURATIONS_TIME = 1;
		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string GenerateToken(UserDataForApp user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Name, user.FullName),
				new Claim(ClaimTypes.Role, user.RoleName),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Actor, user.Avatar == null ? "" : user.Avatar),
				new Claim(ClaimTypes.Name, user.FullName)
			};
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var secretKey = new SymmetricSecurityKey(key);
			var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor {
				Issuer = _configuration.GetSection("Jwt:Issuer").Value,
				Audience = _configuration.GetSection("Jwt:Audience").Value,
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(EXPIRED_DURATIONS_TIME),
				SigningCredentials =  signingCredentials
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
