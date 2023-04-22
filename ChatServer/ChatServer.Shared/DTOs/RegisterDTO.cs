using ChatServer.Shared.Attributes;
using ChatServer.Shared.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs
{
	public class RegisterDTO
	{
		[AppRequired]
		[AppEmail]
		public string Email { get; set; }

		[AppRequired]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[AppRequired]
		[AppConfirmPwd]
		[DataType(DataType.Password)]
		public string ConfirmPwd { get; set; }

		[AppRequired]
		public string FullName { get; set; }
	}
}
