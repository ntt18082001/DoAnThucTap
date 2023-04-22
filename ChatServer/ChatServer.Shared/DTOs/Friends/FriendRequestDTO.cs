using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Friends
{
	public class FriendRequestDTO
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public FriendDTO Sender { get; set; }
	}
}
