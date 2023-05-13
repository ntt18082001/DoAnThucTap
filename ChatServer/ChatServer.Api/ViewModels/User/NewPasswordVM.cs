using ChatServer.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Api.ViewModels.User
{
	public class NewPasswordVM
	{
		public string Email { get; set; }
		public string Code { get; set; }
		[AppRequired]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[AppRequired]
		[AppConfirmPwd("Password")]
		[DataType(DataType.Password)]
		public string ConfirmPwd { get; set; }
	}
}
