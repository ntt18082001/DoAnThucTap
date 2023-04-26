using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class SendMessageDTO
	{
		public int UserId { get; set; }
		public int FriendId { get; set; }
		public string Content { get; set; }
		public IFormFile? File { get; set; }
		public string? Image { get; set; }
	}
}
