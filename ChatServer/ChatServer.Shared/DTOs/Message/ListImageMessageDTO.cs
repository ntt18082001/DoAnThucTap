using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class ListImageMessageDTO
	{
		public int Id { get; set; }
		public int? IdLastMessage { get; set; }
		public int? LengthMessagesImg { get; set; }
		public bool CanGetMore { get; set; }
		public IEnumerable<MessageDTO> Messages { get; set; }
	}
}
