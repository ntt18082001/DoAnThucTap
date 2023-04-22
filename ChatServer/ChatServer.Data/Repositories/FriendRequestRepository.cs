using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.Consts;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Notify;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Repositories
{
	public class FriendRequestRepository : GenericRepository<AppFriendRequest>, IFriendRequestRepository
	{
		public FriendRequestRepository(AppChatDbContext context) : base(context)
		{
		}

		public async Task<bool> FriendRequest(int senderId, int receiverId)
		{
			try
			{
				var friendRequest = new AppFriendRequest();
				friendRequest.SenderId = senderId;
				friendRequest.ReceiverId = receiverId;
				friendRequest.StatusId = StatusRequest.PendingRequest.ID;
				friendRequest.CreatedDate = DateTime.Now;
				await AddAsync(friendRequest);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<NotifyDTO> GetFriendRequest(int senderId, int receiverId)
		{
			var senderFriends = await GetListFriendId(senderId);
			var receiverFriends = await GetListFriendId(receiverId);
			return await dbSet
				.Include(x => x.UserSendRequest)
				.Where(x => x.SenderId == senderId && x.ReceiverId == receiverId)
				.OrderByDescending(x => x.Id)
				.Select(x => new NotifyDTO
				{
					Id = x.Id,
					ReceiverId = receiverId,
					SenderId = x.UserSendRequest.Id,
					FullName = x.UserSendRequest.FullName,
					Avatar = x.UserSendRequest.Avatar,
					MutualFriends = receiverFriends.Intersect(senderFriends).Count(),
					IsAccept = x.StatusId == StatusRequest.AcceptedRequest.ID,
					IsCancel = x.StatusId == StatusRequest.RejectedRequest.ID
				})
				.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<NotifyDTO>> GetListNotify(int id)
		{

			var result = new List<NotifyDTO>();
			var listNotify = await dbSet
				.Include(x => x.UserSendRequest)
				.Include(x => x.UserReceiveRequest)
				.Where(x => (x.ReceiverId == id && x.StatusId == StatusRequest.PendingRequest.ID)
					|| (x.SenderId == id && x.StatusId == StatusRequest.AcceptedRequest.ID))
				.OrderByDescending(x => x.Id)
				.ToListAsync();
			foreach (var item in listNotify)
			{
				var myFriends = await GetListFriendId(item.SenderId);
				var otherFriends = await GetListFriendId(item.ReceiverId);
				result.Add(new NotifyDTO
				{
					Id = item.Id,
					ReceiverId = item.ReceiverId,
					SenderId = item.SenderId,
					Avatar = item.ReceiverId == id ? item.UserSendRequest.Avatar : item.UserReceiveRequest.Avatar,
					FullName = item.ReceiverId == id ? item.UserSendRequest.FullName : item.UserReceiveRequest.FullName,
					MutualFriends = item.ReceiverId == id ? 
						otherFriends.Intersect(myFriends).Count() : myFriends.Intersect(otherFriends).Count(),
					IsAccept = item.StatusId == StatusRequest.AcceptedRequest.ID,
					IsCancel = item.StatusId == StatusRequest.RejectedRequest.ID
				});
			}
			return result;
		}
		public async Task<RequestResult> CancelRequest(int id)
		{
			var request = await dbSet.FindAsync(id);
			if (request != null)
			{
				request.StatusId = StatusRequest.RejectedRequest.ID;
				return new RequestResult
				{
					SenderId = request.SenderId,
					ReceiverId = request.ReceiverId
				};
			}
			return null;
		}

		public async Task<int> CancelRequest(int senderId, int receiverId)
		{
			var request = await dbSet
					.Where(x => x.SenderId == senderId && x.ReceiverId == receiverId
						&& x.StatusId == StatusRequest.PendingRequest.ID)
					.SingleOrDefaultAsync();
			if (request != null)
			{
				request.StatusId = StatusRequest.RejectedRequest.ID;
				return request.Id;
			}
			return 0;
		}

		public async Task<NotifyDTO> AcceptRequest(int id)
		{
			var request = await dbSet
				.Include(x => x.UserReceiveRequest)
				.Where(x => x.Id == id)
				.SingleOrDefaultAsync();
			if (request != null)
			{
				request.StatusId = StatusRequest.AcceptedRequest.ID;

				var friendShip = new AppFriendShip();
				friendShip.UserId1 = request.SenderId;
				friendShip.UserId2 = request.ReceiverId;
				friendShip.CreatedDate = DateTime.Now;
				await _context.AddAsync(friendShip);

				var senderFriends = await GetListFriendId(request.SenderId);
				var receiverFriends = await GetListFriendId(request.ReceiverId);

				return new NotifyDTO
				{
					Id = request.Id,
					SenderId = request.SenderId,
					ReceiverId = request.ReceiverId,
					Avatar = request.UserReceiveRequest.Avatar,
					FullName = request.UserReceiveRequest.FullName,
					MutualFriends = receiverFriends.Intersect(senderFriends).Count(),
					IsAccept = request.StatusId == StatusRequest.AcceptedRequest.ID,
					IsCancel = request.StatusId == StatusRequest.RejectedRequest.ID
				};
			}
			return null;
		}

		public async Task<NotifyDTO> AcceptRequest(int senderId, int receiverId)
		{
			var request = await dbSet
					.Include(x => x.UserReceiveRequest)
					.Where(x => x.SenderId == senderId && x.ReceiverId == receiverId
						&& x.StatusId == StatusRequest.PendingRequest.ID)
					.SingleOrDefaultAsync();
			if (request != null)
			{
				request.StatusId = StatusRequest.AcceptedRequest.ID;

				var friendShip = new AppFriendShip();
				friendShip.UserId1 = request.SenderId;
				friendShip.UserId2 = request.ReceiverId;
				friendShip.CreatedDate = DateTime.Now;
				await _context.AddAsync(friendShip);

				var senderFriends = await GetListFriendId(request.SenderId);
				var receiverFriends = await GetListFriendId(request.ReceiverId);

				return new NotifyDTO
				{
					Id = request.Id,
					SenderId = request.SenderId,
					ReceiverId = request.ReceiverId,
					Avatar = request.UserReceiveRequest.Avatar,
					FullName = request.UserReceiveRequest.FullName,
					MutualFriends = receiverFriends.Intersect(senderFriends).Count(),
					IsAccept = request.StatusId == StatusRequest.AcceptedRequest.ID,
					IsCancel = request.StatusId == StatusRequest.RejectedRequest.ID
				};
			}
			return null;
		}

		public async Task<bool> UnFriend(int senderId, int receiverId)
		{
			var friend = await _context
					.AppFriendsShip
					.Where(x => ((x.UserId1 == senderId && x.UserId2 == receiverId)
						|| (x.UserId1 == receiverId && x.UserId2 == senderId)) && x.DeletedDate == null)
					.SingleOrDefaultAsync();
			if (friend != null)
			{
				friend.DeletedDate = DateTime.Now;
				return true;
			}
			return false;
		}
	}
}
