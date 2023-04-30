using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChatServer.Shared.Consts.DB;

namespace ChatServer.Shared.DTOs.Message
{
	public class UserMessageDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public bool IsOnline { get; set; }
	}
}
