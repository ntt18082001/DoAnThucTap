using ChatServer.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.Api.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private readonly UserService _userService;
		public ChatHub(UserService userService) 
		{
			_userService = userService;
		}
		public override async Task OnConnectedAsync()
		{
			int userId = Convert.ToInt32(Context.UserIdentifier);
			if(userId != null)
			{
				await _userService.SetUserOnline(userId);
				var onlineFriendIds = await _userService.GetListFriendOnline(userId);
				await Clients.Users(onlineFriendIds).SendAsync("FriendConnected", userId);
			}
			await base.OnConnectedAsync();
		}
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			int userId = Convert.ToInt32(Context.UserIdentifier);
			if (userId != null)
			{
				await _userService.SetUserOffline(userId);
				var onlineFriendIds = await _userService.GetListFriendOnline(userId);
				await Clients.Users(onlineFriendIds).SendAsync("FriendDisconnected", userId);
			}
			await base.OnDisconnectedAsync(exception);
		}
	}
}
