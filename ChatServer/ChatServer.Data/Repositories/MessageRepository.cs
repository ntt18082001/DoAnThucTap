using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Repositories
{
	public class MessageRepository : GenericRepository<AppMessage>, IMessageRepository
	{
		public MessageRepository(AppChatDbContext context) : base(context)
		{
		}

		public async Task<bool> CheckExistConversation(int userId, int friendId)
		{
			return await _context
					.AppConversations
					.AnyAsync(x => (x.UserId1 == userId && x.UserId2 == friendId)
					|| (x.UserId1 == friendId && x.UserId2 == userId) && x.DeletedDate == null);
		}

		public async Task<ConversationDTO> SendMessage(SendMessageDTO model)
		{
			try
			{
				var isExistConv = await CheckExistConversation(model.UserId, model.FriendId);
				var conversation = new AppConversation();
				if(isExistConv)
				{
					conversation = await _context
						.AppConversations
						.Where(x => (x.UserId1 == model.UserId && x.UserId2 == model.FriendId)
							|| (x.UserId1 == model.FriendId && x.UserId2 == model.UserId) && x.DeletedDate == null)
						.SingleOrDefaultAsync();
				} else {
					conversation.UserId1 = model.UserId;
					conversation.UserId2 = model.FriendId;
					conversation.StartTime = DateTime.Now;
					conversation.CreatedDate = DateTime.Now;
					await _context.AppConversations.AddAsync(conversation);
					await _context.SaveChangesAsync();
				}

				var message = new AppMessage();
				message.ConversationId = conversation.Id;
				message.SenderId = model.UserId;
				message.ReceiverId = model.FriendId;
				message.CreatedDate = DateTime.Now;
				message.UrlImage = model.Image;
				message.SentTime = DateTime.Now;
				message.Content = model.Content;
				await dbSet.AddAsync(message);
				await _context.SaveChangesAsync();

				var user = await _context
						.AppUsers
						.Where(x => x.Id == model.UserId && x.DeletedDate == null)
						.Select(x => new UserMessageDTO
						{
							Id = x.Id,
							Name = x.FullName,
							Avatar = x.Avatar
						})
						.SingleOrDefaultAsync();
				var friend = await _context
						.AppUsers
						.Where(x => x.Id == model.FriendId && x.DeletedDate == null)
						.Select(x => new UserMessageDTO
						{
							Id = x.Id,
							Name = x.FullName,
							Avatar = x.Avatar
						})
						.SingleOrDefaultAsync();
				var listMsg = await dbSet
						.Where(x => x.ConversationId == conversation.Id && x.DeletedDate == null)
						.Select(x => new MessageDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							SenderId = x.SenderId,
							ReceiverId = x.ReceiverId,
							Content = x.Content,
							UrlImage = x.UrlImage,
							IsLiked = x.IsLiked,
							SentTime = x.SentTime,
							IsSeen = x.IsSeen,
							IsDelete = false
						})
						.ToListAsync();

				return new ConversationDTO
				{
					Id = conversation.Id,
					UserId = model.UserId,
					FriendId = model.FriendId,
					User = user,
					Friend = friend,
					Conversation = listMsg,
					LastMessage = listMsg.LastOrDefault()
				};
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		public async Task<IEnumerable<UserMessageDTO>> GetListFriend(int id, string searchString = null)
		{
			var friends = await GetListFriendId(id);
			var query = _context.AppUsers.AsNoTracking();
			if (searchString != null)
			{
				query = query.Where(x => EF.Functions.Like(x.FullName, $"%{searchString}%"));
			}
			var result = new List<UserMessageDTO>();
			var listUser = await query
					.Where(x => x.Id != id && friends.Contains(x.Id))
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			foreach (var item in listUser)
			{
				result.Add(new UserMessageDTO
				{
					Id = item.Id,
					Name = item.FullName,
					Avatar = item.Avatar,
				});
			}
			return result;
		}
		public async Task<UserMessageDTO> GetUserSelected(int id)
		{
			return await _context
					.AppUsers
					.Where(x => x.Id == id)
					.Select(x => new UserMessageDTO
					{
						Id = x.Id,
						Name = x.FullName,
						Avatar = x.Avatar,
					})
					.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<ConversationDTO>> GetListConversation(int id)
		{
			var result = await _context
					.AppConversations
					.Include(x => x.AppUser1)
					.Include(x => x.AppUser2)
					.Where(x => (x.UserId1 == id || x.UserId2 ==  id) && x.DeletedDate == null)
					.Select(x => new ConversationDTO
					{
						Id = x.Id,
						UserId = x.AppUser1.Id,
						FriendId = x.AppUser2.Id,
						User = new UserMessageDTO
						{
							Id = x.AppUser1.Id,
							Name = x.AppUser1.FullName,
							Avatar = x.AppUser1.Avatar
						},
						Friend = new UserMessageDTO
						{
							Id = x.AppUser2.Id,
							Name = x.AppUser2.FullName,
							Avatar = x.AppUser2.Avatar
						}
					})
					.ToListAsync();
			foreach(var item in result)
			{
				var listMsg = await dbSet
						.Where(x => x.ConversationId == item.Id && x.DeletedDate == null)
						.Select(x => new MessageDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							SenderId = x.SenderId,
							ReceiverId = x.ReceiverId,
							Content = x.Content,
							UrlImage = x.UrlImage,
							IsLiked = x.IsLiked,
							SentTime = x.SentTime,
							IsSeen = x.IsSeen,
							IsDelete = false
						})
						.ToListAsync();
				item.Conversation = listMsg;
				item.LastMessage = listMsg.LastOrDefault();
			}
			return result.OrderByDescending(x => x.LastMessage.SentTime);
		}
	}
}
