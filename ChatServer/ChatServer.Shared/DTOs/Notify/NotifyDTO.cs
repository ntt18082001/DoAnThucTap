using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Notify
{
	public class NotifyDTO
	{
		public int Id { get; set; }
		public int ReceiverId { get; set; }
		public int SenderId { get; set; }
		public string Avatar { get; set; }
		public string FullName { get; set; }
		public int MutualFriends { get; set; }
		public bool IsCancel { get; set; }
		public bool IsAccept { get; set; }
	}
}
