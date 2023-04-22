using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Friends
{
	public class FriendDTO
	{
		public int Id { get; set; }
		public string? FullName { get; set; }
		public string? Avatar { get; set; }
		public int MutualFriends { get; set; }
		public bool IsSendRequest { get; set; }
		public bool IsReceiverRequest { get; set; }
		public bool IsFriendShip { get; set; }
	}
}
