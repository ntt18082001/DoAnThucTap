using AutoMapper;
using ChatServer.Api.Hubs;
using ChatServer.Api.ViewModels.Friend;
using ChatServer.Api.ViewModels.Message;
using ChatServer.Data.Services;
using ChatServer.Shared.DTOs.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatServer.Api.Services.Interfaces;
using System.Security.Claims;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class MessageController : AppControllerBase
	{
		private readonly MessageService _messageService;
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly IFileStorageService _fileStorageService;
		public MessageController(IMapper mapper,
			MessageService messageService, 
			IHubContext<ChatHub> hubContext,
			IFileStorageService fileStorageService) : base(mapper)
		{
			_messageService = messageService;
			_hubContext = hubContext;
			_fileStorageService = fileStorageService;
		}
		[HttpGet("GetListFriend")]
		public async Task<IActionResult> GetListFriend([FromQuery] SearchFriend model)
		{
			try
			{
				var listUser = await _messageService.GetListFriend(model.Id, model.Search);
				return Ok(listUser);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpGet("GetUserSelected/{id}")]
		public async Task<IActionResult> GetUserSelected(int id)
		{
			try
			{
				var user = await _messageService.GetUserSelected(id);
				if(user == null) 
				{
					return Ok(null);
				}
				return Ok(user);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpGet("GetListConversation")]
		public async Task<IActionResult> GetListConversation()
		{
			try
			{
				var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
				var listConv = await _messageService.GetListConversation(Convert.ToInt32(id));
				return Ok(listConv);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("SendMessage")]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO model)
		{
			try
			{
				if (model.File != null)
				{
					var fileName = await _fileStorageService.SaveFile(DEFAULT_FOLDER_AVATAR, model.File);
					model.Image = DEFAULT_FOLDER_AVATAR + "/" + fileName;
				}
				var success = await _messageService.SendMessage(model);
				await _hubContext.Clients.User(model.FriendId.ToString())
						.SendAsync("ReceiveMessage", success);
				return Ok(success);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("SeenMessage")]
		public async Task<IActionResult> SeenMessage([FromBody] SeenMessage model)
		{
			try
			{
				var message = await _messageService.GetMessage(Convert.ToInt32(model.Id));
				if(message == null)
				{
					return BadRequest(ERROR_NAME);
				}
				message.IsSeen = true;
				var isSuccess = await _messageService.UpdateMessage(message);
				if(isSuccess)
				{
					await _hubContext.Clients.User(model.SenderId.ToString())
						.SendAsync("ReceiveSeenMessage", model.Id);
					return Ok(model.Id);
				}
				else
				{
					return BadRequest(ERROR_NAME);
				}
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("ToggleLikeMessage")]
		public async Task<IActionResult> ToggleLikeMessage([FromBody] SeenMessage model)
		{
			try
			{
				var message = await _messageService.GetMessage(Convert.ToInt32(model.Id));
				if (message == null)
				{
					return BadRequest(ERROR_NAME);
				}
				message.IsLiked = !message.IsLiked;
				var isSuccess = await _messageService.UpdateMessage(message);
				if (isSuccess)
				{
					await _hubContext.Clients.Users(model.SenderId.ToString(), model.ReceiverId.ToString())
						.SendAsync("ToggleLikeMessage", model.Id);
					return Ok(true);
				}
				else
				{
					return BadRequest(ERROR_NAME);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("DeleteMessage")]
		public async Task<IActionResult> DeleteMessage([FromBody] SeenMessage model)
		{
			try
			{
				var message = await _messageService.GetMessage(Convert.ToInt32(model.Id));
				if (message == null)
				{
					return BadRequest(ERROR_NAME);
				}
				message.DeletedDate = DateTime.Now;
				var isSuccess = await _messageService.UpdateMessage(message);
				if (isSuccess)
				{
					await _hubContext.Clients.Users(model.SenderId.ToString(), model.ReceiverId.ToString())
						.SendAsync("DeleteMessage", model.Id);
					return Ok(true);
				}
				else
				{
					return BadRequest(ERROR_NAME);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
	}
}
