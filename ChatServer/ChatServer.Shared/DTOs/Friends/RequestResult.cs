using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Friends
{
	public class RequestResult
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
	}
}
