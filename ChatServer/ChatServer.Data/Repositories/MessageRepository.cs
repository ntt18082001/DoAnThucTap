using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.Message;
using ChatServer.Shared.DTOs.Message.Nickname;
using ChatServer.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using XAct;
using XAct.Users;

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
				if (existConv != null)
				{
					conversation = existConv;
				}
				else
				{
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
				if (model.Image != null)
				{
					message.UrlImage = AESThenHMAC.SimpleEncryptWithPassword(model.Image, senderKey);
				}
				else
				{
					message.UrlImage = model.Image;

				}
				message.SentTime = DateTime.Now;
				if (model.Content != null)
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
				var userNickname = await _context
						.AppNicknames
						.Where(x => x.ConversationId == conversation.Id && x.UserId == model.UserId)
						.Select(x => new NicknameDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							UserId = x.UserId,
							Nickname = x.Nickname
						})
						.SingleOrDefaultAsync();
				var friendNickname = await _context
						.AppNicknames
						.Where(x => x.ConversationId == conversation.Id && x.UserId == model.FriendId)
						.Select(x => new NicknameDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							UserId = x.UserId,
							Nickname = x.Nickname
						})
						.SingleOrDefaultAsync();
				var lastMsg = await dbSet
						.AsNoTracking()
						.Where(x => x.ConversationId == conversation.Id && x.IsNotify == false && x.DeletedDate == null)
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
				if (lastMsg.Content != null)
				{
					lastMsg.Content = AESThenHMAC.SimpleDecryptWithPassword(lastMsg.Content, senderKey);
				}
				if (lastMsg.UrlImage != null)
				{
					lastMsg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(lastMsg.UrlImage, senderKey);
				}
				var infoConv = await _context
						.AppInfoConversations
						.Include(x => x.AppColorConversation)
						.Where(x => x.ConversationId == conversation.Id)
						.Select(x => new InfoConversationDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							MainEmoji = x.MainEmoji,
							ColorId = x.ColorId,
							ColorConversation = x.AppColorConversation != null ? new ColorConversationDTO
							{
								Id = x.AppColorConversation.Id,
								TextColorCode = x.AppColorConversation.TextColorCode,
								BackgroundColorCode = x.AppColorConversation.BackgroundColorCode
							} : null
						})
						.SingleOrDefaultAsync();
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
					UserNickname = userNickname,
					FriendNickname = friendNickname
				};
			}
			catch (Exception ex)
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
							.Where(x => x.ConversationId == item.Id && x.IsNotify == false && x.DeletedDate == null)
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
							.Where(x => x.ConversationId == item.Id && x.IsNotify == false && x.DeletedDate == null)
							.CountAsync();
					if (listMsg.Count() < countListMsg) item.CanGetMore = true;
					foreach (var msg in listMsg)
					{
						var key = await GetSenderMessageKey(msg.SenderId);
						if (msg.Content != null)
						{
							msg.Content = AESThenHMAC.SimpleDecryptWithPassword(msg.Content, key);
						}
						if (msg.UrlImage != null)
						{
							msg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(msg.UrlImage, key);
						}
					}
					listMsg.Reverse();
					item.Conversation = listMsg;
					item.LastMessage = listMsg.LastOrDefault();

					var userNickname = await _context
						.AppNicknames
						.Where(x => x.ConversationId == item.Id && x.UserId == item.UserId)
						.Select(x => new NicknameDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							UserId = x.UserId,
							Nickname = x.Nickname
						})
						.SingleOrDefaultAsync();
					var friendNickname = await _context
							.AppNicknames
							.Where(x => x.ConversationId == item.Id && x.UserId == item.FriendId)
							.Select(x => new NicknameDTO
							{
								Id = x.Id,
								ConversationId = x.ConversationId,
								UserId = x.UserId,
								Nickname = x.Nickname
							})
							.SingleOrDefaultAsync();
					item.UserNickname = userNickname;
					item.FriendNickname = friendNickname;
					var infoConv = await _context
						.AppInfoConversations
						.Include(x => x.AppColorConversation)
						.Where(x => x.ConversationId == item.Id)
						.Select(x => new InfoConversationDTO
						{
							Id = x.Id,
							ConversationId = x.ConversationId,
							MainEmoji = x.MainEmoji,
							ColorId = x.ColorId,
							ColorConversation = x.AppColorConversation != null ? new ColorConversationDTO
							{
								Id = x.AppColorConversation.Id,
								TextColorCode = x.AppColorConversation.TextColorCode,
								BackgroundColorCode = x.AppColorConversation.BackgroundColorCode
							} : null
						})
						.SingleOrDefaultAsync();
					item.InfoConversation = infoConv;
				}
				return result.OrderByDescending(x => x.LastMessage.SentTime);
			}
			catch (Exception ex)
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
							.Where(x => x.ConversationId == idConv && x.Id < idLastMsg && x.IsNotify == false && x.DeletedDate == null)
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
					if (msg.Content != null)
					{
						msg.Content = AESThenHMAC.SimpleDecryptWithPassword(msg.Content, key);
					}
					if (msg.UrlImage != null)
					{
						msg.UrlImage = AESThenHMAC.SimpleDecryptWithPassword(msg.UrlImage, key);
					}
				}
				lstMsg.Reverse();
				var countCanGetMore = await dbSet
								.AsNoTracking()
								.Where(x => x.ConversationId == idConv && x.IsNotify == false && x.DeletedDate == null)
								.CountAsync();
				return new GetMoreMessageDTO
				{
					ConversationId = idConv,
					LastMessageId = idLastMsg,
					Messages = lstMsg,
					CanGetMore = (allDataGetted + lstMsg.Count) < countCanGetMore
				};
			}
			catch (Exception ex)
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
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<UpdateEmojiResponseDTO> UpdateInfoConv(UpdateEmojiDTO model)
		{
			try
			{
				AppInfoConversation infoConv;
				if (model.InfoConvId != null)
				{
					infoConv = await _context
							.AppInfoConversations
							.Include(x => x.AppColorConversation)
							.Where(x => x.Id == model.InfoConvId && x.DeletedDate == null)
							.SingleOrDefaultAsync();
				}
				else
				{
					infoConv = new AppInfoConversation();
					infoConv.ConversationId = model.ConversationId;
				}
				infoConv.MainEmoji = model.Emoji;
				if (model.InfoConvId == null)
				{
					await _context.AddAsync(infoConv);
				}

				var message = new AppMessage();
				message.Content = $"{{emoji}} đã đổi biểu cảm thành {model.Emoji}";
				message.SenderId = model.SenderId;
				message.ReceiverId = model.ReceiverId;
				message.ConversationId = model.ConversationId;
				message.IsNotify = true;
				await _context.AddAsync(message);

				await _context.SaveChangesAsync();

				return new UpdateEmojiResponseDTO
				{
					ConversationId = model.ConversationId,
					InfoConversationDTO = new InfoConversationDTO
					{
						Id = infoConv.Id,
						ConversationId = infoConv.ConversationId,
						MainEmoji = infoConv.MainEmoji,
						ColorId = infoConv.ColorId,
						ColorConversation = infoConv.AppColorConversation != null ? new ColorConversationDTO
						{
							Id = infoConv.AppColorConversation.Id,
							TextColorCode = infoConv.AppColorConversation.TextColorCode,
							BackgroundColorCode = infoConv.AppColorConversation.BackgroundColorCode
						} : null
					},
					NotifyMessage = new MessageDTO
					{
						Id = message.Id,
						Content = message.Content,
						IsNotify = message.IsNotify,
						SenderId = message.SenderId,
						ReceiverId = message.ReceiverId
					}
				};
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public async Task<UpdateNicknameResponseDTO> UpdateNickname(UpdateNicknameDTO model)
		{
			try
			{
				AppNickname nickname;
				if (model.NicknameId != null)
				{
					nickname = await _context
							.AppNicknames
							.Where(x => x.ConversationId == model.ConversationId && x.Id == model.NicknameId && x.DeletedDate == null)
							.SingleOrDefaultAsync();
				}
				else
				{
					nickname = new AppNickname();
					nickname.ConversationId = model.ConversationId;
					nickname.UserId = model.UserIdUpdated;
				}
				nickname.Nickname = model.Nickname;
				if (model.NicknameId == null)
				{
					await _context.AddAsync(nickname);
				}
				var message = new AppMessage();
				message.Content = $"{{1}} đã thay đổi biệt danh của {{2}} thành '{model.Nickname}'";
				message.SenderId = model.SenderId;
				message.ReceiverId = model.ReceiverId;
				message.ConversationId = model.ConversationId;
				message.IsNotify = true;
				await _context.AddAsync(message);

				await _context.SaveChangesAsync();

				return new UpdateNicknameResponseDTO
				{
					ConversationId = model.ConversationId,
					Nickname = new NicknameDTO
					{
						Id = nickname.Id,
						ConversationId = nickname.ConversationId,
						UserId = nickname.UserId,
						Nickname = nickname.Nickname
					},
					NotifyMessage = new MessageDTO
					{
						Id = message.Id,
						Content = message.Content,
						IsNotify = message.IsNotify,
						SenderId = message.SenderId,
						ReceiverId = message.ReceiverId,
						UpdatedIdFor = model.UserIdUpdated
					}
				};
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
