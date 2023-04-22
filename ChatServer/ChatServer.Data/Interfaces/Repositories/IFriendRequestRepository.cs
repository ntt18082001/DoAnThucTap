using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces.Repositories
{
	public interface IFriendRequestRepository : IGenericRepository<AppFriendRequest>
	{
		Task<bool> FriendRequest(int senderId, int receiverId);
		Task<NotifyDTO> GetFriendRequest(int senderId, int receiverId);
		Task<IEnumerable<NotifyDTO>> GetListNotify(int id);
		Task<RequestResult> CancelRequest(int id);
		Task<int> CancelRequest(int senderId, int receiverId);
		Task<NotifyDTO> AcceptRequest(int id);
		Task<NotifyDTO> AcceptRequest(int senderId, int receiverId);
		Task<bool> UnFriend(int senderId, int receiverId);
	}
}
