using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces.Repositories
{
	public interface IMessageRepository : IGenericRepository<AppMessage>
	{
		Task<IEnumerable<UserMessageDTO>> GetListFriend(int id, string searchString = null);
		Task<UserMessageDTO> GetUserSelected(int id);
		Task<bool> CheckExistConversation(int userId, int friendId);
		Task<ConversationDTO> SendMessage(SendMessageDTO model);
		Task<IEnumerable<ConversationDTO>> GetListConversation(int id);
	}
}
