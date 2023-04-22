using ChatServer.Data.Interfaces.UnitOfWork;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Services
{
	public class FriendRequestService
	{
		private readonly IUnitOfWork _unitOfWork;
		public FriendRequestService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> FriendRequest(int senderId, int receiverId)
		{
			var isSuccess = await _unitOfWork.FriendRequestRepository.FriendRequest(senderId, receiverId);
			if(isSuccess)
			{
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}
		public async Task<NotifyDTO> GetFriendRequest(int senderId, int receiverId)
		{
			return await _unitOfWork.FriendRequestRepository.GetFriendRequest(senderId, receiverId);
		}
		public async Task<IEnumerable<NotifyDTO>> GetListNotify(int id)
		{
			return await _unitOfWork.FriendRequestRepository.GetListNotify(id);
		}
		public async Task<RequestResult> CancelRequest(int id)
		{
			var isSuccess = await _unitOfWork.FriendRequestRepository.CancelRequest(id);
			if(isSuccess != null)
			{
				await _unitOfWork.SaveAsync();
				return isSuccess;
			}
			return null;
		}
		public async Task<int> CancelRequest(int senderId, int receiverId)
		{
			var requestSuccess = await _unitOfWork.FriendRequestRepository.CancelRequest(senderId, receiverId);
			if(requestSuccess > 0)
			{
				await _unitOfWork.SaveAsync();
				return requestSuccess;
			}
			return 0;
		}
		public async Task<NotifyDTO> AcceptRequest(int id)
		{
			var success = await _unitOfWork.FriendRequestRepository.AcceptRequest(id);
			if(success != null)
			{
				await _unitOfWork.SaveAsync();
				return success;
			}
			return null;
		}
		public async Task<NotifyDTO> AcceptRequest(int senderId, int receiverId)
		{
			var success = await _unitOfWork.FriendRequestRepository.AcceptRequest(senderId, receiverId);
			if (success != null)
			{
				await _unitOfWork.SaveAsync();
				return success;
			}
			return null;
		}
		public async Task<bool> UnFriend(int senderId, int receiverId)
		{
			var isSuccess = await _unitOfWork.FriendRequestRepository.UnFriend(senderId, receiverId);
			if(isSuccess)
			{
				await _unitOfWork.SaveAsync();
				return isSuccess;
			}
			return false;
		}
	}
}
