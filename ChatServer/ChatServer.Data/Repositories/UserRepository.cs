﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.Consts;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.DTOs.Friends;
using ChatServer.Shared.DTOs.User;
using ChatServer.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.PagedList;

namespace ChatServer.Data.Repositories
{
	public class UserRepository : GenericRepository<AppUser>, IUserRepository
	{
		public UserRepository(AppChatDbContext context) : base(context)
		{
		}

		public async Task<AppUser> GetAccountByEmail(string email)
		{
			return await dbSet
				.Where(x => x.Email.Equals(email) && x.DeletedDate == null)
				.OrderByDescending(x => x.Id)
				.SingleOrDefaultAsync();
		}

		public async Task<AppUser> GetUser(int id)
		{
			return await dbSet.Include(x => x.AppRole).SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task<bool> RegisterUser(AppUser user)
		{
			return await AddAsync(user);
		}
		public async Task<IEnumerable<FriendDTO>> GetUserNotFriend(int id, string searchString = null)
		{
			var friends = await GetListFriendId(id);
			var friendRequest = await _context
				.AppFriendsRequests
				.AsNoTracking()
				.Where(x => x.SenderId == id && x.StatusId == StatusRequest.PendingRequest.ID)
				.Select(x => x.ReceiverId)
				.ToListAsync();
			var friendReceiver = await _context
				.AppFriendsRequests
				.AsNoTracking()
				.Where(x => x.ReceiverId == id && x.StatusId == StatusRequest.PendingRequest.ID)
				.Select(x => x.SenderId)
				.ToListAsync();
			var query = dbSet.AsNoTracking();
			if (searchString != null)
			{
				query = query.Where(x => EF.Functions.Like(x.FullName, $"%{searchString}%"));
			}
			var result = new List<FriendDTO>();
			var listUser = await query
					.Where(x => x.Id != id && !friends.Contains(x.Id))
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			foreach(var item in listUser)
			{
				var userFriends = await GetListFriendId(item.Id);
				var mutualFriends = userFriends.Intersect(friends).Count();
				result.Add(new FriendDTO
				{
					Id = item.Id,
					FullName = item.FullName,
					Avatar = item.Avatar,
					MutualFriends = mutualFriends,
					IsFriendShip = friends.Contains(item.Id),
					IsSendRequest = friendRequest.Contains(item.Id),
					IsReceiverRequest = friendReceiver.Contains(item.Id) && !friendRequest.Contains(item.Id)
				});
			}
			return result;
		}
		public async Task<IEnumerable<FriendDTO>> GetListFriend(int id)
		{
			var friends = await GetListFriendId(id);
			var result = new List<FriendDTO>();
			var listUser = await dbSet
					.Where(x => x.Id != id && friends.Contains(x.Id) && x.DeletedDate == null)
					.OrderByDescending(x => x.Id)
					.ToListAsync();
			foreach (var item in listUser)
			{
				var userFriends = await GetListFriendId(item.Id);
				var mutualFriends = userFriends.Intersect(friends).Count();
				result.Add(new FriendDTO
				{
					Id = item.Id,
					FullName = item.FullName,
					Avatar = item.Avatar,
					MutualFriends = mutualFriends,
				});
			}
			return result;
		}

		public async Task<bool> SetUserOnline(int id)
		{
			var user = await dbSet.FindAsync(id);
			if(user != null)
			{
				user.IsOnline = true;
				return true;
			}
			return false;
		}

		public async Task<bool> SetUserOffline(int id)
		{
			var user = await dbSet.FindAsync(id);
			if (user != null)
			{
				user.IsOnline = false;
				return true;
			}
			return false;
		}

		public async Task<List<string>> GetListFriendOnline(int id)
		{
			var friends = await GetListFriendId(id);
			return await dbSet
					.Where(x => x.Id != id && x.IsOnline == true && x.DeletedDate == null && friends.Contains(x.Id))
					.Select(x => x.Id.ToString())
					.ToListAsync();
		}

		public async Task AddVerifyCode(AppVerifyCode code)
		{
			await _context.AddAsync(code);
		}

		public async Task<AppVerifyCode> GetVerifyCode(string code)
		{
			return await _context
				.AppVerifyCodes.SingleOrDefaultAsync(x => x.TokenString.Equals(code));
		}

		public async Task<IPagedList<UserDTO>> GetAllUser(SearchUserDTO search, int currentUserId, int page, int pageSize)
		{
			var query = dbSet.AsNoTracking();
			if (search.Name != null)
			{
				query = query.Where(x => EF.Functions.Like(x.FullName, $"%{search.Name}%"));
			}
			var data = await query.Where(x => x.Id != currentUserId)
					.OrderByDescending(x => x.Id)
					.Select(x => new UserDTO
					{
						Id = x.Id,
						FullName = x.FullName,
						Avatar = x.Avatar,
						Email = x.Email
					})
					.ToPagedListAsync(page, pageSize);
			return data;
		}
	}
}
