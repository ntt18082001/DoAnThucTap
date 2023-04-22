using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Api.Hubs
{
	public class ChatHub : Hub
	{
		public ChatHub() { }
		public async Task SendFriendRequest(string userId)
		{
			string connectionId = Context.ConnectionId;

			await Clients.User(userId).SendAsync("ReceiveFriendRequest", connectionId);
		}
	}
}
