using ChatServer.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs
{
	public class LoginDTO
	{
		[AppRequired]
		[AppEmail]
		public string Email { get; set; }
		[AppRequired]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
