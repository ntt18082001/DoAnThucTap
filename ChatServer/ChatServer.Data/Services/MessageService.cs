using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.UnitOfWork;
using ChatServer.Shared.DTOs.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Services
{
	public class MessageService
	{
		private readonly IUnitOfWork _unitOfWork;
		public MessageService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<UserMessageDTO>> GetListFriend(int id, string searchString = null)
		{
			return await _unitOfWork.MessageRepository.GetListFriend(id, searchString);
		}
		public async Task<UserMessageDTO> GetUserSelected(int id)
		{
			return await _unitOfWork.MessageRepository.GetUserSelected(id);
		}
		public async Task<bool> CheckConversation(int userId, int friendId)
		{
			return await _unitOfWork.MessageRepository.CheckExistConversation(userId, friendId);
		}
		public async Task<ConversationDTO> SendMessage(SendMessageDTO model)
		{
			return await _unitOfWork.MessageRepository.SendMessage(model);
		}
		public async Task<IEnumerable<ConversationDTO>> GetListConversation(int id)
		{
			return await _unitOfWork.MessageRepository.GetListConversation(id);
		}
		public async Task<AppMessage> GetMessage(int id)
		{
			return await _unitOfWork.MessageRepository.FindAsync(x => x.Id == id);
		}
		public async Task<bool> UpdateMessage(AppMessage message)
		{
			if(message != null)
			{
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		} 
	}
}
