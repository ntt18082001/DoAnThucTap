using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.User
{
	public class UpdateUserDTO
	{
		public int Id { get; set; }
		public string? FullName {get; set;}
		public string? PhoneNumber {get; set;}
		public string? Address {get; set;}
	}
}
