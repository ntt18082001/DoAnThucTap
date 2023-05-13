using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class ColorConversationDTO
	{
		public int Id { get; set; }
		public string BackgroundColorCode { get; set; }
		public string TextColorCode { get; set; }
	}
}
