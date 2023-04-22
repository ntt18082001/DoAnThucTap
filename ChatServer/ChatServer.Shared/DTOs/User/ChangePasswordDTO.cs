using ChatServer.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.User
{
	public class ChangePasswordDTO
	{
		public int Id { get; set; }
		[AppRequired]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[AppRequired]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[AppRequired]
		[AppConfirmPwd("NewPassword")]
		[DataType(DataType.Password)]
		public string ConfirmNewPassword { get; set; }
	}
}
