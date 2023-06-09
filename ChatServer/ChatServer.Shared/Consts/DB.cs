﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Consts
{
	public static class DB
	{
		public static class AppRole
		{
			public const string TABLE_NAME = "AppRole";
			public const short NAME_LENGTH = 50;
			public const short DESC_LENGTH = 100;
		}
		public static class AppUser
		{
			public const string TABLE_NAME = "AppUser";
			public const short USERNAME_LENGTH = 200;
			public const short PWD_LENGTH = 200;
			public const short FULLNAME_LENGTH = 50;
			public const short PHONE_LENGTH = 20;
			public const short EMAIL_LENGTH = 200;
			public const short ADDRESS_LENGTH = 100;
			public const short AVATAR_LENGTH = 200;
			public const short MESSAGE_KEY_LENGTH = 100;
		}
		public static class AppConversation
		{
			public const string TABLE_NAME = "AppConversation";
		}
		public static class AppMessage
		{
			public const string TABLE_NAME = "AppMessage";
			public const short CONTENT_LENGTH = 5000;
			public const short IMAGE_LENGTH = 200;
		}
		public static class MstStatusRequest
		{
			public const string TABLE_NAME = "MstStatusRequest";
			public const short STATUS_NAME_LENGTH = 200;
		}
		public static class AppFriendRequest
		{
			public const string TABLE_NAME = "AppFriendRequest";
		}
		public static class AppFriendShip
		{
			public const string TABLE_NAME = "AppFriendShip";
		}
		public static class AppColorConversation
		{
			public const string TABLE_NAME = "AppColorConversation";
			public const short BG_COLOR_CODE_LENGTH = 200;
			public const short TEXT_COLOR_CODE_LENGTH = 200;
		}
		public static class AppInfoConversation
		{
			public const string TABLE_NAME = "AppInfoConversation";
			public const short NICKNAME_LENGTH = 500;
			public const short EMOJI_LENGTH = 200;
		}
		public static class AppVerifyCode
		{
			public const string TABLE_NAME = "AppVerifyCode";
			public const string DEFAULT_DATE = "GETDATE()";
			public const bool IS_VERIFIED = false;
		}
		public static class AppNickname
		{
			public const string TABLE_NAME = "AppNickname";
			public const short NICKNAME_LENGTH = 500;
		}
	}
}
