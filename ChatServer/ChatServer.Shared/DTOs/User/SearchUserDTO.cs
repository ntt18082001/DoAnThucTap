﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.User
{
	public class SearchUserDTO
	{
		public string? Name { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
