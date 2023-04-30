using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppUser : AppEntityBase
	{
		public AppUser()
		{
			ConversationsOfUser1 = new HashSet<AppConversation>();
			ConversationsOfUser2 = new HashSet<AppConversation>();
			SendMessage = new HashSet<AppMessage>();
			ReceiveMessage = new HashSet<AppMessage>();
			SendRequests = new HashSet<AppFriendRequest>();
			ReceiveRequests = new HashSet<AppFriendRequest>();
			AppFriends1 = new HashSet<AppFriendShip>();
			AppFriends2 = new HashSet<AppFriendShip>();
		}
		public string? Username { get; set; }
		public byte[]? PasswordHash { get; set; }
		public byte[]? PasswordSalt { get; set; }
		public string? FullName { get; set; }
		public string? PhoneNumber1 { get; set; }
		public string? PhoneNumber2 { get; set; }
		public string? Email { get; set; }
		public string? Address { get; set; }
		public string? Avatar { get; set; }
		public DateTime? BlockedTo { get; set; }
		public int? BlockedBy { get; set; }
		public bool IsOnline { get; set; }
		public int? AppRoleId { get; set; }
		public string? MessageKey { get; set; }
		public AppRole AppRole { get; set; }
		public ICollection<AppConversation> ConversationsOfUser1 { get; set; }
		public ICollection<AppConversation> ConversationsOfUser2 { get; set; }

		public ICollection<AppMessage> SendMessage { get; set; }
		public ICollection<AppMessage> ReceiveMessage { get; set; }

		public ICollection<AppFriendRequest> SendRequests { get; set; }
		public ICollection<AppFriendRequest> ReceiveRequests { get; set; }

		public ICollection<AppFriendShip> AppFriends1 { get; set; }
		public ICollection<AppFriendShip> AppFriends2 { get; set; }
	}
}
