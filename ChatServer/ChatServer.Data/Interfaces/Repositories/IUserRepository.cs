﻿using AutoMapper;
using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.DTOs.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<AppUser>
	{
		Task<AppUser> GetUser(int id);
		Task<bool> RegisterUser(AppUser user);
		Task<AppUser> GetAccountByEmail(string email);
		Task<IEnumerable<FriendDTO>> GetUserNotFriend(int id, string searchString = null);
		Task<IEnumerable<FriendDTO>> GetListFriend(int id);
		Task<bool> SetUserOnline(int id);
		Task<bool> SetUserOffline(int id);
		Task<List<string>> GetListFriendOnline(int id);
	}
}
