using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs.Message;
using ChatServer.Shared.DTOs.Message.Nickname;
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
		Task<AppConversation> GetConversation(int userId, int friendId);
		Task<ConversationDTO> SendMessage(SendMessageDTO model);
		Task<IEnumerable<ConversationDTO>> GetListConversation(int id);
		Task<string> GetSenderMessageKey(int id);
		Task<GetMoreMessageDTO> GetMoreMessage(int idConv, int idLastMsg, int allDataGetted);
		Task<ListImageMessageDTO> GetListImgMessage(int id, int? idLastMsg, int lengthData = 0);
		Task<UpdateEmojiResponseDTO> UpdateInfoConv(UpdateEmojiDTO model);
		Task<UpdateNicknameResponseDTO> UpdateNickname(UpdateNicknameDTO model);
	}
}
