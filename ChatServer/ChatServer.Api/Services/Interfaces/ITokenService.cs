using ChatServer.Api.ViewModels.User;

namespace ChatServer.Api.Services.Interfaces
{
	public interface ITokenService
	{
		string GenerateToken(UserDataForApp user);
	}
}
