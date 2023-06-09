﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.User
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string? FullName { get; set; }
		public string? Avatar { get; set; }
		public string? Email { get; set; }
	}
}
