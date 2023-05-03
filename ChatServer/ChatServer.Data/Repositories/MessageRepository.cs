﻿using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Message;
using ChatServer.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace ChatServer.Data.Repositories
{
	public class MessageRepository : GenericRepository<AppMessage>, IMessageRepository
	{
		public MessageRepository(AppChatDbContext context) : base(context)
		{
		}

		public async Task<AppConversation> GetConversation(int userId, int friendId)
		{
			return await _context
					.AppConversations
					.Where(x => ((x.UserId1 == userId && x.UserId2 == friendId)
					|| (x.UserId1 == friendId && x.UserId2 == userId)) && x.DeletedDate == null)
					.SingleOrDefaultAsync();
		}

		public async Task<ConversationDTO> SendMessage(SendMessageDTO model)
		{
			try
			{
				var existConv = await GetConversation(model.UserId, model.FriendId);
				var conversation = new AppConversation();
				if(existConv != null)
				{
					conversation = existConv;
				} else {
					conversation.UserId1 = model.UserId;
					conversation.UserId2 = model.FriendId;
					conversation.StartTime = DateTime.Now;
					conversation.CreatedDate = DateTime.Now;
					await _context.AppConversations.AddAsync(conversation);
					await _context.SaveChangesAsync();
				}

				var senderKey = await GetSenderMessageKey(model.UserId);

				var message = new AppMessage();
				message.ConversationId = conversation.Id;
				message.SenderId = model.UserId;
				message.ReceiverId = model.FriendId;
				message.CreatedDate = DateTime.Now;
				if(model.Image != null)
				{
					message.UrlImage = AESThenHMAC.SimpleEncryptWithPassword(model.Image, senderKey);
				} else
				{
					message.UrlImage = model.Image;

				}
				message.SentTime = DateTime.Now;
				if(model.Content != null)
				{
					message.Content = AESThenHMAC.SimpleEncryptWithPassword(model.Content, senderKey);
				}
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
							Avatar = x.Avatar,
							IsOnline = x.IsOnline
						})
						.SingleOrDefaultAsync();
				var lastMsg = await dbSet
						.AsNoTracking()
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
						.OrderByDescending(x => x.Id)
						.FirstOrDefaultAsync();
				if(lastMsg.Content != null)
				{
					lastMsg.Content = AESThenHMAC.SimpleDecryptWithPassword(lastMsg.Content, senderKey);
				}
				if(lastMsg.UrlImage!= null)
				{
					lastMsg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(lastMsg.UrlImage, senderKey);
				}
				var listMsg = new List<MessageDTO>();
				listMsg.Add(lastMsg);
				return new ConversationDTO
				{
					Id = conversation.Id,
					UserId = model.UserId,
					FriendId = model.FriendId,
					User = user,
					Friend = friend,
					Conversation = listMsg,
					LastMessage = lastMsg,
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
					IsOnline = item.IsOnline
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
						IsOnline = x.IsOnline
					})
					.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<ConversationDTO>> GetListConversation(int id)
		{
			try
			{
				var result = await _context
					.AppConversations
					.AsNoTracking()
					.Include(x => x.AppUser1)
					.Include(x => x.AppUser2)
					.Where(x => (x.UserId1 == id || x.UserId2 == id) && x.DeletedDate == null)
					.Select(x => new ConversationDTO
					{
						Id = x.Id,
						UserId = x.UserId1 == id ? x.UserId1 : x.UserId2,
						FriendId = x.UserId1 == id ? x.UserId2 : x.UserId1,
						User = new UserMessageDTO
						{
							Id = x.UserId1 == id ? x.AppUser1.Id : x.AppUser2.Id,
							Name = x.UserId1 == id ? x.AppUser1.FullName : x.AppUser2.FullName,
							Avatar = x.UserId1 == id ? x.AppUser1.Avatar : x.AppUser2.Avatar
						},
						Friend = new UserMessageDTO
						{
							Id = x.UserId1 == id ? x.AppUser2.Id : x.AppUser1.Id,
							Name = x.UserId1 == id ? x.AppUser2.FullName : x.AppUser1.FullName,
							Avatar = x.UserId1 == id ? x.AppUser2.Avatar : x.AppUser1.Avatar,
							IsOnline = x.UserId1 == id ? x.AppUser2.IsOnline : x.AppUser1.IsOnline
						}
					})
					.ToListAsync();
				foreach (var item in result)
				{
					var listMsg = await dbSet
							.AsNoTracking()
							.Include(x => x.Sender)
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
							.OrderByDescending(x => x.Id)
							.Take(20)
							.ToListAsync();
					var countListMsg = await dbSet
							.AsNoTracking()
							.Include(x => x.Sender)
							.Where(x => x.ConversationId == item.Id && x.DeletedDate == null)
							.CountAsync();
					if (listMsg.Count() < countListMsg) item.CanGetMore = true;
					foreach (var msg in listMsg)
					{
						var key = await GetSenderMessageKey(msg.SenderId);
						if(msg.Content != null)
						{
							msg.Content = AESThenHMAC.SimpleDecryptWithPassword(msg.Content, key);
						}
						if(msg.UrlImage != null)
						{
							msg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(msg.UrlImage, key);
						}
					}
					listMsg.Reverse();
					item.Conversation = listMsg;
					item.LastMessage = listMsg.LastOrDefault();
				}
				return result.OrderByDescending(x => x.LastMessage.SentTime);
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		public async Task<string> GetSenderMessageKey(int id)
		{
			return await _context
						.AppUsers
						.Where(x => x.Id == id && x.DeletedDate == null)
						.Select(x => x.MessageKey)
						.SingleOrDefaultAsync();
		}

		public async Task<GetMoreMessageDTO> GetMoreMessage(int idConv, int idLastMsg, int allDataGetted)
		{
			try
			{
				var lstMsg = await dbSet
							.AsNoTracking()
							.Where(x => x.ConversationId == idConv && x.Id < idLastMsg && x.DeletedDate == null)
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
							.OrderByDescending(x => x.Id)
							.Take(10)
							.ToListAsync();
				foreach (var msg in lstMsg)
				{
					var key = await GetSenderMessageKey(msg.SenderId);
					if(msg.Content != null)
					{
						msg.Content = AESThenHMAC.SimpleDecryptWithPassword(msg.Content, key);
					}
					if(msg.UrlImage != null)
					{
						msg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(msg.UrlImage, key);
					}
				}
				lstMsg.Reverse();
				var countCanGetMore = await dbSet
								.AsNoTracking()
								.Where(x => x.ConversationId == idConv && x.DeletedDate == null)
								.CountAsync();
				return new GetMoreMessageDTO
				{
					ConversationId = idConv,
					LastMessageId = idLastMsg,
					Messages = lstMsg,
					CanGetMore = (allDataGetted + lstMsg.Count) < countCanGetMore
				};
			}
			catch(Exception ex)
			{
				return null;
			}
		}
		public async Task<ListImageMessageDTO> GetListImgMessage(int id, int? idLastMsg, int lengthData = 0)
		{
			try
			{
				IEnumerable<MessageDTO> mesgs;
				if (idLastMsg.HasValue)
				{
					mesgs = await dbSet
								.AsNoTracking()
								.Where(x => x.ConversationId == id && x.Id < idLastMsg && x.UrlImage != null && x.DeletedDate == null)
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
								.OrderByDescending(x => x.Id)
								.ToListAsync();
				}
				else
				{
					mesgs = await dbSet
								.AsNoTracking()
								.Where(x => x.ConversationId == id && x.UrlImage != null && x.DeletedDate == null)
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
								.OrderByDescending(x => x.Id)
								.ToListAsync();
				}
				foreach (var msg in mesgs)
				{
					var key = await GetSenderMessageKey(msg.SenderId);
					if (msg.UrlImage != null)
					{
						msg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(msg.UrlImage, key);
					}
				}
				mesgs.Reverse();
				var countCanGetMore = await dbSet
									.AsNoTracking()
									.Where(x => x.ConversationId == id && x.UrlImage != null && x.DeletedDate == null)
									.CountAsync();
				return new ListImageMessageDTO
				{
					Id = id,
					IdLastMessage = mesgs.LastOrDefault().Id,
					LengthMessagesImg = lengthData + mesgs.Count(),
					Messages = mesgs,
					CanGetMore = (lengthData + mesgs.Count()) < countCanGetMore
				};
			}
			catch(Exception ex)
			{
				return null;
			}
		}
	}
}
