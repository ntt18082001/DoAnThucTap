﻿namespace ChatServer.Api.ViewModels.User
{
	public class UserDataForApp
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber1 { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }
		public string Address { get; set; }
		public string RoleName { get; set; }
		public DateTime? CreatedDate { get; set; }
		public int? AppRoleId { get; set; }
	}
}
