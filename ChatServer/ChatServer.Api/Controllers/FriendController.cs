using AutoMapper;
using Azure.Core;
using ChatServer.Api.Hubs;
using ChatServer.Api.ViewModels.Friend;
using ChatServer.Api.ViewModels.Notify;
using ChatServer.Api.WebConfig;
using ChatServer.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class FriendController : AppControllerBase
	{
		private readonly UserService _userService;
		private readonly FriendRequestService _friendRequestService;
		private readonly IHubContext<ChatHub> _hubContext;
		public FriendController(IMapper mapper,
			UserService userService, IHubContext<ChatHub> hubContext,
			FriendRequestService friendRequestService) : base(mapper)
		{
			_userService = userService;
			_hubContext = hubContext;
			_friendRequestService = friendRequestService;
		}
		[HttpGet("GetListUserNotFriend")]
		public async Task<IActionResult> GetListUserNotFriend([FromQuery] SearchFriend model)
		{
			try
			{
				var listUser = await _userService.GetListUserNotFriend(model.Id, model.Search);
				return Ok(listUser);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("AddFriend")]
		public async Task<IActionResult> AddFriend(AddFriend addFriend)
		{
			try
			{
				var isSuccess = await _friendRequestService.FriendRequest(addFriend.SenderId, addFriend.ReceiverId);
				if (isSuccess)
				{
					var request = await _friendRequestService.GetFriendRequest(addFriend.SenderId, addFriend.ReceiverId);
					await _hubContext.Clients.User(addFriend.ReceiverId.ToString()).SendAsync("ReceiveFriendRequest", request);
					return Ok(request.ReceiverId);
				}
				return Ok(false);
			} catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpGet("GetListNotify")]
		public async Task<IActionResult> GetListNotify()
		{
			try
			{
				var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
				var listNotify = await _friendRequestService.GetListNotify(Convert.ToInt32(id));
				return Ok(listNotify);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("CancelRequest/{id}")]
		public async Task<IActionResult> CancelRequest(int id)
		{
			try
			{
				var requestSuccess = await _friendRequestService.CancelRequest(id);
				await _hubContext.Clients.User(requestSuccess.SenderId.ToString())
					.SendAsync("CancelRequestFromReceiver", requestSuccess.ReceiverId);
				return Ok(requestSuccess.SenderId);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("CancelRequestFromSender")]
		public async Task<IActionResult> CancelRequestFromSender(CancelRequest model)
		{
			try
			{
				var requestSuccess = await _friendRequestService.CancelRequest(model.SenderId, model.ReceiverId);
				var result = new
				{
					NotifyId = requestSuccess,
					SenderId = model.SenderId
				};
				await _hubContext.Clients.User(model.ReceiverId.ToString()).SendAsync("CancelRequestFromSender", result);
				return Ok(model.ReceiverId);
			} catch (Exception ex)
			{
				return Ok(false);
			}
		}
		[HttpPut("CancelRequestFromReceiver")]
		public async Task<IActionResult> CancelRequestFromReceiver(CancelRequest model)
		{
			try
			{
				var requestSuccess = await _friendRequestService.CancelRequest(model.SenderId, model.ReceiverId);
				var result = new
				{
					NotifyId = requestSuccess,
					SenderId = model.SenderId
				};
				await _hubContext.Clients.User(model.SenderId.ToString())
					.SendAsync("CancelRequestFromReceiver", model.ReceiverId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return Ok(false);
			}
		}
		[HttpPut("AcceptRequestFromNotify/{id}")]
		public async Task<IActionResult> AcceptRequestFromNotify(int id)
		{
			try
			{
				var requestSuccess = await _friendRequestService.AcceptRequest(id);
				await _hubContext.Clients.User(requestSuccess.SenderId.ToString())
					.SendAsync("AcceptRequest", requestSuccess);
				return Ok(requestSuccess.SenderId);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("AcceptRequest")]
		public async Task<IActionResult> AcceptRequest(AcceptRequest model)
		{
			try
			{
				var success = await _friendRequestService.AcceptRequest(model.SenderId, model.ReceiverId);
				await _hubContext.Clients.User(success.SenderId.ToString())
					.SendAsync("AcceptRequest", success);
				var result = new
				{
					NotifyId = success.Id,
					SenderId = model.SenderId
				};
				return Ok(result);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPut("unfriend")]
		public async Task<IActionResult> UnFriend(Unfriend model)
		{
			try
			{
				var isSuccess = await _friendRequestService.UnFriend(model.SenderId, model.ReceiverId);
				await _hubContext.Clients.Users(model.SenderId.ToString(), model.ReceiverId.ToString())
					.SendAsync("Unfriend", model);
				return Ok(isSuccess);
			}
			catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		} 
	}
}
